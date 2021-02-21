using System;
using System.IO;
using Meebey.SmartIrc4net;
using System.Linq;
using BasicTwitchSoundPlayer.Structs;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace BasicTwitchSoundPlayer.IRC
{
    class IRCBot
    {
        public bool BotRunning;
        private OldIRCClient irc;
        private MainForm parent;
        private static ReadMessage FormattedMessage;
        private string channelToJoin;
        private char PrefixChar;

        private SoundBase SndDB { get; set; }
        PrivateSettings programSettings;

        public IRCBot(MainForm parentReference, PrivateSettings programSettings, SoundBase soundDb, char PrefixChar)
        {
            irc = new OldIRCClient(parentReference, programSettings.TwitchServer, programSettings.TwitchUsername, programSettings.TwitchPassword, programSettings.TwitchChannelToJoin, programSettings.SoundRewardID, programSettings.TTSRewardID);
            this.programSettings = programSettings;
            channelToJoin = programSettings.TwitchChannelToJoin;
            parent = parentReference;
            this.PrefixChar = PrefixChar;
            SndDB = soundDb;
        }

        public void Run()
        {
            InitBot(channelToJoin);
            this.BotRunning = true;
            try
            {
                irc.meebyIrc.Listen();
            }
            catch(ThreadInterruptedException e)
            {
                Debug.WriteLine("Nop" + e);
            }
        }

        public void StopBot()
        {
            SndDB.Close();
            programSettings.SaveSettings();
            irc.SaveIgnoredList();
            irc.SaveTrustedList();
            irc.SaveSuperMods();

            irc.meebyIrc.OnError -= MeebyIrc_OnError;
            irc.meebyIrc.OnErrorMessage -= MeebyIrc_OnErrorMessage;
            irc.meebyIrc.OnConnecting -= MeebyIrc_OnConnecting;
            irc.meebyIrc.OnConnected -= MeebyIrc_OnConnected;
            irc.meebyIrc.OnAutoConnectError -= MeebyIrc_OnAutoConnectError;
            irc.meebyIrc.OnDisconnecting -= MeebyIrc_OnDisconnecting;
            irc.meebyIrc.OnDisconnected -= MeebyIrc_OnDisconnected;
            irc.meebyIrc.OnRegistered -= MeebyIrc_OnRegistered;
            irc.meebyIrc.OnPart -= MeebyIrc_OnPart;
            irc.meebyIrc.OnJoin -= MeebyIrc_OnJoin;
            irc.meebyIrc.OnChannelAction -= MeebyIrc_OnChannelAction;
            irc.meebyIrc.OnReadLine -= MeebyIrc_OnReadLine;
            irc.meebyIrc.OnRawMessage -= MeebyIrc_OnRawMessage;

            Task.Factory.StartNew(() =>
                irc.meebyIrc.Disconnect()
            );

            System.Threading.Thread.Sleep(200);
        }

        internal void TestStack()
        {
            SndDB.PlaySoundIfExists("1", "cheeki_breeki", TwitchRightsEnum.Admin);
            Thread.Sleep(50);
            SndDB.PlaySoundIfExists("2", "whawhawhaa", TwitchRightsEnum.Admin);


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
            irc.meebyIrc.OnOp += MeebyIrc_OnOp;
            irc.meebyIrc.OnDeop += MeebyIrc_OnDeop;
            irc.meebyIrc.OnRawMessage += MeebyIrc_OnRawMessage;

            //Request capabilities - https://dev.twitch.tv/docs/irc/guide/#twitch-irc-capabilities
            irc.meebyIrc.WriteLine("CAP REQ :twitch.tv/tags twitch.tv/commands twitch.tv/membership");
            irc.meebyIrc.RfcJoin("#" + channel);
        }

        private void MeebyIrc_OnRawMessage(object sender, IrcEventArgs e)
        {
            try
            {
                if(e.Data.Channel != null && e.Data.Nick != null && e.Data.Message != null)
                {
                    ReadMessage msg = new ReadMessage();
                    if (e.Data.Tags.ContainsKey("custom-reward-id"))
                    {
                        if (programSettings.TTSLogic == TTSLogic.RewardIDAndCommand || programSettings.SoundRedemptionLogic == SoundRedemptionLogic.ChannelPoints)
                        {
                            var rewardID = e.Data.Tags["custom-reward-id"];
                            msg.userID = e.Data.Tags["user-id"];

                            if (rewardID == programSettings.TTSRewardID)
							{
                                msg.msgType = MessageType.TTSReward;
                                msg.RewardID = rewardID;
                            }
                            else if (rewardID == programSettings.SoundRewardID)
							{
                                msg.msgType = MessageType.SoundReward;
                                msg.RewardID = rewardID;
                            }
                            else
							{
                                msg.msgType = MessageType.Normal;
                                msg.RewardID = "";
                            }
                        }
                        else
                            msg.msgType = MessageType.Normal;

                    }
                    msg.user = e.Data.Nick;
                    msg.message = e.Data.Message;
                    msg.rights = GetRoleFromTags(e);
                    bool result = RunBot(msg);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                Logger.AddLine("Exception on Raw Message: " + ex.ToString());
            }

        }

        private TwitchRightsEnum GetRoleFromTags(IrcEventArgs e)
        {
            if (irc.supermod.Contains(e.Data.Nick))
                return TwitchRightsEnum.Admin;
            else
            {
                if (e.Data.Tags != null)
                {
                    //Ref: https://dev.twitch.tv/docs/irc/tags/
                    if (e.Data.Tags.ContainsKey("badges") && e.Data.Tags["badges"].Contains("broadcaster/1"))
                        return TwitchRightsEnum.Admin;
                    if (e.Data.Tags.ContainsKey("mod") && e.Data.Tags["mod"] == "1")
                        return TwitchRightsEnum.Mod;
                    else if (e.Data.Tags.ContainsKey("badges") && e.Data.Tags["badges"].Contains("vip/1"))
                        return TwitchRightsEnum.TrustedSub;
                    else if (e.Data.Tags.ContainsKey("badges") && e.Data.Tags["badges"].Contains("subscriber/1"))
                        return TwitchRightsEnum.TrustedSub;
                    else
                        return TwitchRightsEnum.Public;
                }
                else
                    return TwitchRightsEnum.Public;
            }
        }

        private bool RunBot(ReadMessage formattedMessage)
        {
            FormattedMessage = formattedMessage;

            switch(FormattedMessage.msgType)
            {
                case MessageType.TTSReward:
                    {
                        parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.SoundCommand);

                        if (programSettings.TTSLogic == TTSLogic.Restricted)
                        {
                            if (FormattedMessage.rights >= programSettings.TTSRoleRequirement)
                            {
                                if (SndDB.PlayTTS(formattedMessage.user, formattedMessage.message, true))
                                    irc.UpdateRedemptionStatus(formattedMessage, KrakenConnections.RedemptionStates.FULFILLED);
                                else
                                    irc.UpdateRedemptionStatus(formattedMessage, KrakenConnections.RedemptionStates.CANCELED);
                                return true;
                            }
                            else
                                return true;
                        }
                        else
                        {
                            SndDB.PlayTTS(formattedMessage.user, formattedMessage.message, true);
                            return true;
                        }
                    }
                case MessageType.SoundReward:
                    {
                        parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.SoundCommand);
                        string text = formattedMessage.message.ToLower();
                        if(text.StartsWith(PrefixChar.ToString()))
                            text = text.Remove(0, 1);

                        TwitchRightsEnum privilage = formattedMessage.rights;
                        

                        if (SndDB.PlaySoundIfExists(formattedMessage.user, text, privilage, true))
                            irc.UpdateRedemptionStatus(formattedMessage, KrakenConnections.RedemptionStates.FULFILLED);
                        else
                            irc.UpdateRedemptionStatus(formattedMessage, KrakenConnections.RedemptionStates.CANCELED);

                        return true;
                    }
                default:
                    if (!formattedMessage.message.StartsWith(PrefixChar.ToString()) || irc.ignorelist.Contains(formattedMessage.user))
                    {
                        parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.Generic);
                        //literally nothing else happens in your code if this is false
                        return true;
                    }
                    else
                    {
                        TwitchRightsEnum privilage = formattedMessage.rights;
                        string text = formattedMessage.message.Remove(0, 1).ToLower();

                        //Mod Commands
                        if (formattedMessage.rights >= TwitchRightsEnum.Mod || irc.moderators.Contains(formattedMessage.user))
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

                            if (text.StartsWith("suboverride"))
                            {
                                parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.ModCommand);
                                SndDB.ChangeSubOverride(irc, text);
                                return true;
                            }

                        }

                        //TTS
                        if (text.StartsWith("tts "))
                        {
                            parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.SoundCommand);

                            if (programSettings.TTSLogic == TTSLogic.RewardIDAndCommand)
                            {
                                if (formattedMessage.rights >= TwitchRightsEnum.Mod)
                                    SndDB.PlayTTS(formattedMessage.user, formattedMessage.message.Split(new char[] { ' ' }, 2)[1]);
                                return true;
                            }

                            return true;
                        } //SoundPlayback
                        else
                        {
                            if(programSettings.SoundRedemptionLogic == SoundRedemptionLogic.Legacy || privilage == TwitchRightsEnum.Admin)
                            {
                                parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.SoundCommand);
                                SndDB.PlaySoundIfExists(formattedMessage.user, text, privilage);
                            }
                        }

                        return true;
                    }
            }
        }

        internal void UpdateVolume()
        {
            SndDB.ChangeVolume(programSettings.Volume);
        }

        #region EventHandlers
        private void MeebyIrc_OnJoin(object sender, Meebey.SmartIrc4net.JoinEventArgs e)
        {
            parent.ThreadSafeAddPreviewText("! JOINED: " + e.Data.Nick, LineType.IrcCommand);
        }

        private void MeebyIrc_OnRegistered(object sender, EventArgs e)
        {
            parent.ThreadSafeAddPreviewText("! LOGIN VERIFIED", LineType.IrcCommand);
            if(programSettings.SoundRedemptionLogic == SoundRedemptionLogic.ChannelPoints)
			{
                
			}
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
