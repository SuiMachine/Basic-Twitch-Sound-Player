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
        private string KRAKENURI { get; set; }
        private string TwitchAuthy { get; set; }
        private string Channel { get; set; }
            
        public KrakenConnections(string Channel, string TwitchAuthy)
        {
            this.TwitchAuthy = "OAuth " + TwitchAuthy;
            this.Channel = Channel;
            KRAKENURI = @"https://api.twitch.tv/kraken/channels/" + Channel.ToLower() + "/";
        }

        #region Async
        public async Task<string[]> GetSubscribersAsync()
        {
            string response = await GetNewUpdateAsync("subscriptions");
            if (response == null || response == "")
            {
                return new string[0];
            }
            else
            {
                JObject jReader = JObject.Parse(response);
                if (jReader["subscriptions"] != null)
                {
                    var jSubs = jReader["subscriptions"];
                    string[] subscribers = new string[jSubs.Count()];
                    for (int i = 0; i < subscribers.Length; i++)
                    {
                        var jSub = jSubs.ElementAt(i);
                        var user = jSub["user"];
                        subscribers[i] = user["name"].Value<string>();
                    }
                    return subscribers;
                }
            }

            return new string[0];
        }

        private async Task<string> GetNewUpdateAsync(string scope)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(KRAKENURI + scope);

            try
            {
                request.Headers["Client-ID"] = "9z58zy6ak0ejk9lme6dy6nyugydaes";
                request.Headers["Authorization"] = TwitchAuthy;
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
