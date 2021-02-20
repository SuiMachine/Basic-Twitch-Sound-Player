using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BasicTwitchSoundPlayer.IRC
{
    class KrakenConnections
    {
        [Serializable]
        public class ChannelAward
		{
            public string id = "";
            public bool is_enabled = false;
            public int cost = 0;
            public string title = "";
            public bool is_user_input_required = false;
            public bool is_paused = false;
            public bool should_redemptions_skip_request_queue = false;
        }

        private string HELIXURI { get; set; }
        private string TwitchAuthy { get; set; }
        private string Channel { get; set; }
        private string BroadcasterID { get; set; }

        private const string BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID = "9z58zy6ak0ejk9lme6dy6nyugydaes";

        public KrakenConnections(string Channel, string TwitchAuthy)
        {
            this.TwitchAuthy = TwitchAuthy;
            this.Channel = Channel;
            //KRAKENURI = "https://api.twitch.tv/kraken/channels/" + Channel.ToLower() + "/";
            HELIXURI = "https://api.twitch.tv/helix/";
        }

        #region Async
        public async Task<string[]> GetSubscribersAsync()
        {
            if(BroadcasterID == null || BroadcasterID == "")
            {
                string responseID = await GetNewUpdateAsync("users", "?login=" + Channel, true);
                if (responseID == null || responseID == "")
                    return new string[0];
                else
                {
                    JObject jReader = JObject.Parse(responseID);
                    if (jReader["data"] != null)
                    {
                        var dataNode = jReader["data"];
                        var firstDataChild = dataNode.First;
                        if (firstDataChild["id"] != null)
                        {
                            BroadcasterID = firstDataChild["id"].ToString();
                        }
                        else
                            return new string[0];
                    }
                    else
                        return new string[0];
                }
            }

            string response = await GetNewUpdateAsync("subscriptions", "?broadcaster_id=" + BroadcasterID, true);
            if (response == null || response == "")
            {
                return new string[0];
            }
            else
            {
                JObject jReader = JObject.Parse(response);
                if (jReader["data"] != null)
                {
                    var dataNode = jReader["data"];

                    List<string> subscribers = new List<string>();

                    foreach(var sub in dataNode)
                    {
                        if(sub["user_name"] != null)
                        {
                            var curSub = sub["user_name"].ToString();
                            if (!subscribers.Contains(curSub))
                                subscribers.Add(curSub);
                        }
                    }
                    return subscribers.ToArray();
                }
            }

            return new string[0];
        }

        public async Task VerifyChannelRewardsAsync(MainForm mainForm, string soundredemptionid, string ttsredemptionid)
        {
			{
                int endTimer = 5;
                while((BroadcasterID == null || BroadcasterID == "") && endTimer >= 0)
				{
                    await Task.Delay(1000);
                    endTimer--;
                }
            }


            if (BroadcasterID == null || BroadcasterID == "")
			{
                mainForm.ThreadSafeAddPreviewText("[ERROR] No broadcaster ID to verify Channel Rewards!", LineType.IrcCommand);
                return;
			}

            string response = await GetNewUpdateAsync("channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, true);
            if(response == null || response == "")
			{
                mainForm.ThreadSafeAddPreviewText("[ERROR] Incorrect response when verifying Channel Rewards!", LineType.IrcCommand);
                return;
            }
            else
			{
                JObject jReader = JObject.Parse(response);
                if (jReader["data"] != null)
                {
                    ChannelAward soundredemptionReward = null;
                    ChannelAward ttsredemptionReward = null;

                    var dataNode = jReader["data"];
                    foreach(var customReward in dataNode)
					{
                        var parseNode = customReward.ToObject<ChannelAward>();
                        if (soundredemptionid != null && parseNode.id.Equals(soundredemptionid, StringComparison.InvariantCultureIgnoreCase))
                            soundredemptionReward = parseNode;
                        else if (ttsredemptionid != null && parseNode.id.Equals(ttsredemptionid, StringComparison.InvariantCultureIgnoreCase))
                            ttsredemptionReward = parseNode;
                    }

                    if(soundredemptionid != null && soundredemptionid != "")
					{
                        if(soundredemptionReward == null)
						{
                            mainForm.ThreadSafeAddPreviewText("[ERROR] Haven't found Channel Reward with Sound Reward ID!", LineType.IrcCommand);
                        }
                        else
						{
                            if (!soundredemptionReward.is_enabled)
                                mainForm.ThreadSafeAddPreviewText("[WARNING] Sound reward is not enabled!", LineType.IrcCommand);
                            else if(soundredemptionReward.is_paused)
                                mainForm.ThreadSafeAddPreviewText("[WARNING] Sound reward redemption is paused!", LineType.IrcCommand);
                            else if(!soundredemptionReward.is_user_input_required)
                                mainForm.ThreadSafeAddPreviewText("[ERROR] Sound reward is incorrectly configured - it needs to require player input!", LineType.IrcCommand);
                            else if (soundredemptionReward.should_redemptions_skip_request_queue)
                                mainForm.ThreadSafeAddPreviewText("[WARNING] Sound redemption via points shouldn't skip request queue to allow for returning points, if request fails!", LineType.IrcCommand);
                        }
                    }

                    if (ttsredemptionid != null && ttsredemptionid != "")
                    {
                        if (ttsredemptionReward == null)
                        {
                            mainForm.ThreadSafeAddPreviewText("[ERROR] Haven't found Channel Reward with TTS Reward ID!", LineType.IrcCommand);
                        }
                        else
						{
                            if (!ttsredemptionReward.is_enabled)
                                mainForm.ThreadSafeAddPreviewText("[WARNING] TTS reward is not enabled!", LineType.IrcCommand);
                            else if (ttsredemptionReward.is_paused)
                                mainForm.ThreadSafeAddPreviewText("[WARNING] TTS reward redemption is paused!", LineType.IrcCommand);
                            else if (!ttsredemptionReward.is_user_input_required)
                                mainForm.ThreadSafeAddPreviewText("[ERROR] TTS reward is incorrectly configured - it needs to require player input!", LineType.IrcCommand);
                            else if (ttsredemptionReward.should_redemptions_skip_request_queue)
                                mainForm.ThreadSafeAddPreviewText("[WARNING] TTS redemption via points shouldn't skip request queue to allow for returning points, if request fails!", LineType.IrcCommand);
                        }
                    }

                    //Clear up redemption queue
                    if(ttsredemptionReward != null || soundredemptionReward != null)
					{
                        //channel_points/custom_rewards/redemptions?broadcaster_id=274637212&reward_id=92af127c-7326-4483-a52b-b0da0be61c01&id=17fa2df1-ad76-4804-bfa5-a40ef63efe63
                        if(soundredemptionReward != null)
						{
                            string response2 = await GetNewUpdateAsync("channel_points/custom_rewards/redemptions", "?broadcaster_id=" + BroadcasterID, true);

                        }
                    }
                }
            }

        }

        private async Task<string> GetNewUpdateAsync(string scope, string parameters = "", bool RequireBearerToken = false)
        {
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(HELIXURI + scope + parameters);

            try
            {
                request.Headers["Client-ID"] = BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID;
                if(!RequireBearerToken)
                    request.Headers["Authorization"] = "OAuth " + TwitchAuthy;
                else
                    request.Headers["Authorization"] = "Bearer " + TwitchAuthy;

                request.Timeout = 5000;
                request.Method = "GET";


                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return "";
            }
        }


        #endregion
    }
}
