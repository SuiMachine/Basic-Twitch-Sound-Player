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
        private string HELIXURI { get; set; }
        private string TwitchAuthy { get; set; }
        private string Channel { get; set; }
        private string BroadcasterID { get; set; }
            
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

        private async Task<string> GetNewUpdateAsync(string scope, string parameters = "", bool RequireBearerToken = false)
        {
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(HELIXURI + scope + parameters);

            try
            {
                request.Headers["Client-ID"] = "9z58zy6ak0ejk9lme6dy6nyugydaes";
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
