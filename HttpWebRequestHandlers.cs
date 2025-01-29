using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BasicTwitchSoundPlayer
{
	internal static class HttpWebRequestHandlers
	{
		/// <summary>
		/// Does a request and gets a Json response.
		/// </summary>
		/// <param name="address">Url to perform a request on</param>
		/// <param name="result">JSON recevied as response (or empty if failed)</param>
		/// <returns>True if response was sucessful, false if failed.</returns>
		public static bool GrabJson(Uri address, out string result)
		{
			try
			{
				HttpWebRequest wRequest = (HttpWebRequest)HttpWebRequest.Create(address);
				wRequest.Credentials = CredentialCache.DefaultCredentials;

				dynamic wResponse = wRequest.GetResponse().GetResponseStream();
				StreamReader reader = new StreamReader(wResponse);
				result = reader.ReadToEnd();
				reader.Close();
				wResponse.Close();
				return true;
			}
			catch (Exception)
			{
				result = "";
				return false;
			}

		}

		/// <summary>
		/// A more complex function for during HTTP requests.
		/// </summary>
		/// <param name="address">Address to perform HTTP requests on</param>
		/// <param name="headers">Dictionary for HTTP headers</param>
		/// <param name="contantType">Content type</param>
		/// <param name="acceptStr">Accept typestring</param>
		/// <param name="Method">Method</param>
		/// <param name="result">JSON returned of request (or empty on failed)</param>
		/// <returns>True if succeeds, False if failed.</returns>
		public static bool PerformGetRequest(Uri address, Dictionary<string, string> headers, out string result)
		{
			try
			{
				HttpWebRequest wRequest = (HttpWebRequest)HttpWebRequest.Create(address);

				//Headers
				if (headers != null)
				{
					foreach (var header in headers)
					{
						wRequest.Headers[header.Key] = header.Value;
					}
				}

				wRequest.ContentType = "application/json";
				wRequest.Accept = "application/vnd.twitchtv.v3+json";
				wRequest.Method = "GET";

				dynamic wResponse = wRequest.GetResponse().GetResponseStream();
				StreamReader reader = new StreamReader(wResponse);
				result = reader.ReadToEnd();
				reader.Close();
				wResponse.Close();
				return true;
			}
			catch (Exception)
			{
				result = "";
				return false;
			}
		}

		public static bool PerformDelete(Uri address, Dictionary<string, string> headers, out string result)
		{
			try
			{
				WebRequest wRequest = WebRequest.Create(address);

				//Headers
				if (headers != null)
				{
					foreach (var header in headers)
					{
						wRequest.Headers[header.Key] = header.Value;
					}
				}
				wRequest.Method = "DELETE";

				dynamic wResponse = wRequest.GetResponse().GetResponseStream();
				StreamReader reader = new StreamReader(wResponse);
				result = reader.ReadToEnd();
				reader.Close();
				wResponse.Close();
				return true;
			}
			catch (Exception ex)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine("Error doing Delete: " + ex.ToString());
#endif
				result = "";
				return false;
			}
		}

		public static bool PerformTwitchPost(Uri address, Dictionary<string, string> headers, string postData, out string result)
		{
			try
			{
				HttpWebRequest wRequest = (HttpWebRequest)HttpWebRequest.Create(address);

				//Headers
				if (headers != null)
				{
					foreach (var header in headers)
					{
						wRequest.Headers[header.Key] = header.Value;
					}
				}

				byte[] encodedPostData = Encoding.UTF8.GetBytes(postData);
				wRequest.ContentType = "application/json";
				wRequest.Accept = "application/vnd.twitchtv.v3+json";
				wRequest.Method = "POST";
				wRequest.ContentLength = encodedPostData.LongLength;

				Stream requestStream = wRequest.GetRequestStream();
				requestStream.Write(encodedPostData, 0, encodedPostData.Length);
				requestStream.Close();

				dynamic wResponse = wRequest.GetResponse().GetResponseStream();
				StreamReader reader = new StreamReader(wResponse);
				result = reader.ReadToEnd();
				reader.Close();
				wResponse.Close();
				return true;
			}
			catch (Exception ex)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine("Error doing Post: " + ex.ToString());
#endif
				result = "";
				return false;
			}
		}

		public static async System.Threading.Tasks.Task<string> PerformPost(Uri address, Dictionary<string, string> headers, string postData)
		{
			try
			{
				HttpWebRequest wRequest = (HttpWebRequest)HttpWebRequest.Create(address);

				//Headers
				if (headers != null)
				{
					foreach (var header in headers)
					{
						wRequest.Headers[header.Key] = header.Value;
					}
				}

				byte[] encodedPostData = Encoding.UTF8.GetBytes(postData);
				wRequest.ContentType = "application/json";
				wRequest.Method = "POST";
				wRequest.ContentLength = encodedPostData.LongLength;

				Stream requestStream = wRequest.GetRequestStream();
				await requestStream.WriteAsync(encodedPostData, 0, encodedPostData.Length);
				requestStream.Close();

				dynamic wResponse = wRequest.GetResponse().GetResponseStream();
				StreamReader reader = new StreamReader(wResponse);
				var result = await reader.ReadToEndAsync();
				reader.Close();
				wResponse.Close();
				return result;
			}
			catch (Exception ex)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine("Error doing Post: " + ex.ToString());
#endif
				return null;
			}
		}


		/// <summary>
		/// A wrapper function for building a URL requests
		/// </summary>
		/// <param name="BaseURI">Base of URL</param>
		/// <param name="variables">Parameters to build on</param>
		/// <returns>Combined URL</returns>
		internal static Uri BuildRequestUri(string BaseURI, string[] variables)
		{
			string uri = BaseURI;
			for (int i = 0; i < variables.Length; i++)
			{
				if (i == 0)
				{
					uri += "?" + variables[i];
				}
				else
				{
					uri += "&" + variables[i];
				}
			}
			return new Uri(uri);
		}

		/// <summary>
		/// Formats parameter to key=Value
		/// </summary>
		/// <param name="header">Header</param>
		/// <param name="variable">Variable</param>
		/// <returns>Formatted parameter</returns>
		internal static string FormatParameter(string header, string variable)
		{
			return header + "=" + Uri.EscapeDataString(variable);
		}
	}

}
