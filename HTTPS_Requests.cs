using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer
{
	public static class HTTPS_Requests
	{
		public static async Task<string> PostAsync(string baseUrl, string scope, string parameters, string jsonContent, Dictionary<string, string> requestHeaders)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl + scope + parameters);
			request.ContentType = "application/json";
			request.Method = "POST";

			try
			{
				foreach (var requestHeader in requestHeaders)
					request.Headers[requestHeader.Key] = requestHeader.Value;

				using (var streamWriter = new StreamWriter(await request.GetRequestStreamAsync()))
				{
					streamWriter.Write(jsonContent);
				}

				var httpResponse = (HttpWebResponse)await request.GetResponseAsync();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
					return result;
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				MessageBox.Show(e.Message);
				return "";
			}
		}

		public static async Task<string> PatchAsync(string baseUri, string scope, string parameters, string jsonContent, Dictionary<string, string> Headers)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUri + scope + parameters);
			request.ContentType = "application/json";
			request.Method = "PATCH";

			try
			{
				foreach (var header in Headers)
					request.Headers[header.Key] = header.Value;

				using (var streamWriter = new StreamWriter(await request.GetRequestStreamAsync()))
				{
					streamWriter.Write(jsonContent);
				}

				var httpResponse = (HttpWebResponse)await request.GetResponseAsync();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
					return result;
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				MainForm.Instance.ThreadSafeAddPreviewText($"Error with patch request: {e.Message}", LineType.IrcCommand);
				return "";
			}
		}

		public static async Task<string> GetAsync(string baseUrl, string scope, string parameters, Dictionary<string, string> requestHeaders, bool returnErrorCode = false)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl + scope + parameters);

			try
			{
				foreach (var requestHeader in requestHeaders)
					request.Headers[requestHeader.Key] = requestHeader.Value;

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
				if (returnErrorCode)
				{
					return "Error: " + e.Message;
				}
				return "";
			}
		}

		public static async Task<HttpStatusCode> DeleteAsync(string baseUrl, string scope, string parameters, Dictionary<string, string> requestHeaders)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl + scope + parameters);
			request.ContentType = "application/json";
			request.Method = "DELETE";

			try
			{
				/*				request.Headers["Client-ID"] = BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID;
								if (!RequireBearerToken)
									request.Headers["Authorization"] = "OAuth " + PrivateSettings.GetInstance().UserAuth;
								else
									request.Headers["Authorization"] = "Bearer " + PrivateSettings.GetInstance().UserAuth;*/
				foreach (var requestHeader in requestHeaders)
					request.Headers[requestHeader.Key] = requestHeader.Value;

				var httpResponse = (HttpWebResponse)await request.GetResponseAsync();
				return httpResponse.StatusCode;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				MessageBox.Show(e.Message);
				return HttpStatusCode.BadRequest;
			}
		}

	}
}
