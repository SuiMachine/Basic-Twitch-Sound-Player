using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BasicTwitchSoundPlayer.IRC
{
    class KrakenConnections
    {
        private string KRAKENURI { get; set; }
            
        public KrakenConnections(string Channel)
        {
            KRAKENURI = @"https://api.twitch.tv/kraken/" + Channel.ToLower();
        }

        public async Task<string[]> GetSubscribersAsync()
        {
            string response = await GetNewUpdateAsync();
            if (response == null || response == "")
            {
                return null;
            }
            else
            {
                var temp = response;
            }

            return new string[0];
        }

        private async Task<string> GetNewUpdateAsync()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(KRAKENURI);
                request.Timeout = 5000;

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch
            {
                return "";
            }

        }
    }
}
