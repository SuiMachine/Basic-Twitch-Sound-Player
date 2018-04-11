using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using Meebey.SmartIrc4net;
using System.Linq;

namespace BasicTwitchSoundPlayer.IRC
{
    //Some of the functions here may be a bit weird. Often the reason is, they were changed to use SmartIRC4Net from my original TCP socket client
    struct ReadMessage
    {
        public string user;
        public string message;
    }

    class OldIRCClient
    {
        MainForm parent;
        public bool ConnectedStatus = true;
        static string ignoredfile = "ignored_users.txt";
        static string trustedfile = "trusted_users.txt";
        static string supermodsfile = "supermods.txt";
        #region properties
        private string Config_Server { get; set; }
        private string Config_Username { get; set; }
        private string Config_Password { get; set; }
        private string Config_Channel { get; set; }
        #endregion

        public List<string> supermod = new List<string>();
        public List<string> moderators = new List<string>();
        public List<string> ignorelist = new List<string>();
        public List<string> trustedUsers = new List<string>();
        public string[] subscribers = new string[0];
        private KrakenConnections krakenConnection;
        private Timer krakenUpdateTimer;

        //Because I really don't want to rewrite half of this
        public IrcClient meebyIrc = new IrcClient();

        #region Constructor
        public OldIRCClient(MainForm parentReference, string Server, string Username, string Password, string Channel)
        {
            parent = parentReference;
            Config_Server = Server;
            Config_Username = Username;
            Config_Password = Password;
            Config_Channel = Channel;


            meebyIrc.Encoding = System.Text.Encoding.UTF8;
            meebyIrc.SendDelay = 200;
            meebyIrc.AutoRetry = true;
            meebyIrc.AutoReconnect = true;

            try
            {
                meebyIrc.Connect(Config_Server, 6667);
                while (!meebyIrc.IsConnected)
                    System.Threading.Thread.Sleep(50);
                meebyIrc.Login(Config_Username, Config_Username, 4, Config_Username, "oauth:" + Config_Password);
                krakenConnection = new KrakenConnections(Channel, Password);
                krakenUpdateTimer = new Timer();
                krakenUpdateTimer.Start();
                krakenUpdateTimer.Elapsed += KrakenUpdateTimer_Elapsed;
                krakenUpdateTimer.Interval = 10;

            }
            catch (ConnectionException e)
            {
                parent.ThreadSafeAddPreviewText("Could not connect! Reason:" + e.Message, LineType.IrcCommand);
            }

            LoadSuperMods(Channel.ToLower());
            LoadIgnoredList();
            LoadTrustedList();
        }

        private void KrakenUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            krakenUpdateTimer.Interval = 2 * 60 * 1000;

            Task<string[]> updateTask = krakenConnection.GetSubscribersAsync();
            Debug.WriteLine("Updating subscribers");
            updateTask.Wait();
            var result = updateTask.Result;
            if(result != null)
            {
                int PreviousSubscribers = subscribers.Length;
                if(PreviousSubscribers != result.Length)
                {
                    parent.ThreadSafeAddPreviewText("Subscriber amount changed to " + result.Length, LineType.IrcCommand);
                }
                subscribers = result;
            }
        }
        #endregion

        #region BasicFunctions
        public void SendChatMessage(string message)
        {
            meebyIrc.SendMessage(SendType.Message, "#" + Config_Channel, message);
        }

        public void SendChatMessage_NoDelays(string message)
        {
            int originalDelay = meebyIrc.SendDelay;
            meebyIrc.SendDelay = 0;
            meebyIrc.SendMessage(SendType.Message, "#" + Config_Channel, message);
            meebyIrc.SendDelay = originalDelay;
        }

        #endregion

        #region SuperModLoadSave
        private void LoadSuperMods(string channel)
        {
            supermod.Clear();
            supermod.Add(channel);
            moderators.Add(channel);
            if (File.Exists(supermodsfile))
            {
                StreamReader SR = new StreamReader(supermodsfile);
                string line = "";

                while ((line = SR.ReadLine()) != null)
                {
                    if (line != "" && !supermod.Contains(line.ToLower()))
                    {
                        supermod.Add(line.ToLower());
                        if (!moderators.Contains(line.ToLower()))
                            moderators.Add(line.ToLower());
                    }
                }
                SR.Close();
                SR.Dispose();
            }
        }

        public void SaveSuperMods()
        {
            File.WriteAllLines(supermodsfile, supermod);
        }
        #endregion

        #region trustedUsers
        public void TrustedUserAdd(ReadMessage msg)
        {
            if (moderators.Contains(msg.user))
            {
                string[] helper = msg.message.Split(new char[] { ' ' }, 2);
                if (!moderators.Contains(helper[1].ToLower()))
                {
                    if (!trustedUsers.Contains(helper[1].ToLower()))
                    {
                        trustedUsers.Add(helper[1].ToLower());
                        SaveTrustedList();
                        SendChatMessage("Added " + helper[1] + " to trusted list.");
                    }
                    else
                    {
                        SendChatMessage(helper[1] + " is already on trusted list.");
                    }
                }
            }
        }

        public void TrustedUsersRemove(ReadMessage msg)
        {
            if (moderators.Contains(msg.user))
            {
                string[] helper = msg.message.Split(new char[] { ' ' }, 2);

                if (trustedUsers.Contains(helper[1].ToLower()))
                {
                    trustedUsers.Remove(helper[1].ToLower());
                    SaveTrustedList();
                    SendChatMessage("Removed " + helper[1] + " from trusted list.");
                }
                else
                {
                    SendChatMessage(helper[1] + " is not present on trusted list.");
                }
            }
        }

        private void LoadTrustedList()
        {
            trustedUsers.Clear();
            if (File.Exists(@trustedfile))
            {
                StreamReader SR = new StreamReader(@trustedfile);
                string line = "";

                while ((line = SR.ReadLine()) != null)
                {
                    if (line != "")
                        trustedUsers.Add(line.ToLower());
                }
                SR.Close();
                SR.Dispose();
            } 
        }

        public void SaveTrustedList()
        {
            File.WriteAllLines(trustedfile, trustedUsers);
        }
        #endregion

        #region IgnoredList
        public void IgnoreListAdd(ReadMessage msg)
        {
            if(moderators.Contains(msg.user))
            {                                
                string[] helper = msg.message.Split(new char[] { ' ' }, 2);
                if (!moderators.Contains(helper[1].ToLower()))
                {
                    if (!ignorelist.Contains(helper[1].ToLower()))
                    {
                        ignorelist.Add(helper[1].ToLower());
                        SaveIgnoredList();
                        SendChatMessage("Added " + helper[1] + " to ignored list.");
                    }
                    else
                    {
                        SendChatMessage(helper[1] + " is already on ignored list.");
                    }
                }
                else
                    SendChatMessage("Moderators can't be added to ignored list!");

            }
        }

        public void IgnoreListRemove(ReadMessage msg)
        {
            if (moderators.Contains(msg.user))
            {
                string[] helper = msg.message.Split(new char[] { ' ' }, 2);

                    if (ignorelist.Contains(helper[1].ToLower()))
                    {
                        ignorelist.Remove(helper[1].ToLower());
                        SaveIgnoredList();
                        SendChatMessage("Removed " + helper[1] + " from ignored list.");
                    }
                    else
                    {
                        SendChatMessage(helper[1] + " is not present on ignored list.");
                    }
            }
        }

        public void LoadIgnoredList()
        {
            ignorelist.Clear();
            if(File.Exists(@ignoredfile))
            {
                StreamReader SR = new StreamReader(@ignoredfile);
                string line = "";

                while ((line = SR.ReadLine()) != null)
                {
                    if (line != "")
                        ignorelist.Add(line.ToLower());
                }
                SR.Close();
                SR.Dispose();
            } 
        }

        public void SaveIgnoredList()
        {
            File.WriteAllLines(@ignoredfile, ignorelist);
        }
        #endregion
    }
}
