using System;
using System.IO;
using Meebey.SmartIrc4net;
using System.Linq;
using BasicTwitchSoundPlayer.Structs;

namespace BasicTwitchSoundPlayer.IRC
{
    class IRCBot
    {
        public bool BotRunning;
        private OldIRCClient irc;
        private MainForm parent;
        private static ReadMessage FormattedMessage;
        private string channelToJoin;
        private char prefixChar;
        private SoundBase SndDB { get; set; }
        PrivateSettings programSettings;

        public IRCBot(MainForm parentReference, PrivateSettings programSettings, SoundBase soundDb)
        {
            irc = new OldIRCClient(parentReference, programSettings.TwitchServer, programSettings.TwitchUsername, programSettings.TwitchPassword, programSettings.TwitchChannelToJoin);
            this.programSettings = programSettings;
            channelToJoin = programSettings.TwitchChannelToJoin;
            parent = parentReference;
            prefixChar = '-';
            SndDB = soundDb;
        }

        public void Run()
        {
            InitBot(channelToJoin);
            irc.meebyIrc.Listen();
        }

        public void StopBot()
        {
            SndDB.Close();
            programSettings.SaveSettings();
            irc.SaveIgnoredList();
            irc.SaveTrustedList();
            irc.SaveSuperMods();
            irc.meebyIrc.Disconnect();
        }

        private void InitBot(string channel)
        {
            irc.meebyIrc.OnError += MeebyIrc_OnError;
            irc.meebyIrc.OnErrorMessage += MeebyIrc_OnErrorMessage;
            irc.meebyIrc.OnConnecting += MeebyIrc_OnConnecting;
            irc.meebyIrc.OnConnected += MeebyIrc_OnConnected;
            irc.meebyIrc.OnAutoConnectError += MeebyIrc_OnAutoConnectError;
            irc.meebyIrc.OnDisconnecting += MeebyIrc_OnDisconnecting;
            irc.meebyIrc.OnDisconnected += MeebyIrc_OnDisconnected;
            irc.meebyIrc.OnRegistered += MeebyIrc_OnRegistered;
            irc.meebyIrc.OnPart += MeebyIrc_OnPart;
            irc.meebyIrc.OnJoin += MeebyIrc_OnJoin;
            irc.meebyIrc.OnChannelAction += MeebyIrc_OnChannelAction;
            irc.meebyIrc.OnReadLine += MeebyIrc_OnReadLine;
            irc.meebyIrc.OnChannelMessage += MeebyIrc_OnChannelMessage;
            irc.meebyIrc.OnOp += MeebyIrc_OnOp;
            irc.meebyIrc.OnDeop += MeebyIrc_OnDeop;
            irc.meebyIrc.WriteLine("CAP REQ :twitch.tv/membership");
            irc.meebyIrc.RfcJoin("#" + channel);
        }

        private static bool Check(string toc)
        {
            if (FormattedMessage.message.StartsWith(toc, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        private bool RunBot(ReadMessage formattedMessage)
        {
            FormattedMessage = formattedMessage;

            if (!formattedMessage.message.StartsWith(prefixChar.ToString()) || irc.ignorelist.Contains(formattedMessage.user))
            {
                parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, (int)LineType.Generic);
                //literally nothing else happens in your code if this is false
                return true;
            }
            else
            {
                TwitchRights privilage = GetPrivilageForUser(formattedMessage.user);
                string text = formattedMessage.message.Remove(0, 1).ToLower();

                if (irc.moderators.Contains(formattedMessage.user))
                {
                    if (text == "volume" || text.StartsWith("volume "))
                    {
                        parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.ModCommand);
                        SndDB.ChangeVolumeIRC(irc, text, parent);
                        return true;
                    }

                    if (text == "delay" || text.StartsWith("delay "))
                    {
                        parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.ModCommand);
                        SndDB.ChangeDelay(irc, text);
                        return true;
                    }

                    if (text == "stopallsounds")
                    {
                        parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.ModCommand);
                        SndDB.Stopallsounds();
                        return true;
                    }

                    if (text.StartsWith("removesound "))
                    {
                        parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.ModCommand);
                        SndDB.RemoveSound(irc, text);
                        return true;
                    }

                }
                parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.SoundCommand);
                SndDB.PlaySoundIfExists(formattedMessage.user, text, privilage);
                return true;
            }

        }

        internal void UpdateVolume()
        {
            SndDB.ChangeVolume(programSettings.Volume);
        }

        private TwitchRights GetPrivilageForUser(string user)
        {
            if (irc.supermod.Contains(user))
                return TwitchRights.Admin;
            else if (irc.moderators.Contains(user))
                return TwitchRights.Mod;
            else if (irc.subscribers.Contains(user) || irc.trustedUsers.Contains(user))
                return TwitchRights.TrustedSub;
            else
                return TwitchRights.Public;
        }

        #region EventHandlers
        private void MeebyIrc_OnJoin(object sender, Meebey.SmartIrc4net.JoinEventArgs e)
        {
            parent.ThreadSafeAddPreviewText("! JOINED: " + e.Data.Nick, LineType.IrcCommand);
        }

        private void MeebyIrc_OnRegistered(object sender, EventArgs e)
        {
            parent.ThreadSafeAddPreviewText("! LOGIN VERIFIED", LineType.IrcCommand);
        }

        private void MeebyIrc_OnDisconnected(object sender, EventArgs e)
        {
            parent.ThreadSafeAddPreviewText("Disconnected.", LineType.IrcCommand);
            BotRunning = false;
        }

        private void MeebyIrc_OnDisconnecting(object sender, EventArgs e)
        {
            parent.ThreadSafeAddPreviewText("Disconnecting...", LineType.IrcCommand);
        }

        private void MeebyIrc_OnAutoConnectError(object sender, Meebey.SmartIrc4net.AutoConnectErrorEventArgs e)
        {
            parent.ThreadSafeAddPreviewText("OnAutoConnectError Event: " + e.Exception, LineType.IrcCommand);
        }

        private void MeebyIrc_OnConnecting(object sender, EventArgs e)
        {
            parent.ThreadSafeAddPreviewText("Connecting: " + e, LineType.IrcCommand);
        }

        private void MeebyIrc_OnConnected(object sender, EventArgs e)
        {
            parent.ThreadSafeAddPreviewText("Connected: " + e, LineType.IrcCommand);
        }

        private void MeebyIrc_OnChannelAction(object sender, Meebey.SmartIrc4net.ActionEventArgs e)
        {
            parent.ThreadSafeAddPreviewText("OnChannelAction Event: " + e.Data, LineType.IrcCommand);
        }

        private void MeebyIrc_OnErrorMessage(object sender, Meebey.SmartIrc4net.IrcEventArgs e)
        {
            parent.ThreadSafeAddPreviewText("! " + e.Data.Message + " !", LineType.IrcCommand);
        }

        private void MeebyIrc_OnError(object sender, Meebey.SmartIrc4net.ErrorEventArgs e)
        {
            parent.ThreadSafeAddPreviewText("OnError Event: " + e.Data.Message, LineType.IrcCommand);
        }

        private void MeebyIrc_OnReadLine(object sender, Meebey.SmartIrc4net.ReadLineEventArgs e)
        {
            //Console.WriteLine("onReadLine Event:" + e.Line);
        }

        private void MeebyIrc_OnPart(object sender, PartEventArgs e)
        {
            parent.ThreadSafeAddPreviewText("! PART: " + e.Data.Nick, LineType.IrcCommand);
        }

        private void MeebyIrc_OnChannelMessage(object sender, IrcEventArgs e)
        {
            ReadMessage msg;
            msg.user = e.Data.Nick;
            msg.message = e.Data.Message;
            RunBot(msg);
        }

        private void MeebyIrc_OnOp(object sender, OpEventArgs e)
        {
            if (!irc.moderators.Contains(e.Whom))
                irc.moderators.Add(e.Whom);
            parent.ThreadSafeAddPreviewText("! +OP: " + e.Whom, LineType.IrcCommand);
        }

        private void MeebyIrc_OnDeop(object sender, DeopEventArgs e)
        {
            if (!irc.supermod.Contains(e.Whom))                  //Ignore if the user is supermod
                irc.moderators.Remove(e.Whom);
            parent.ThreadSafeAddPreviewText("! -OP: " + e.Whom, LineType.IrcCommand);
        }
        #endregion
    }
}
