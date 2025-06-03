using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Xml.Serialization;

namespace BasicTwitchSoundPlayer.Structs.Gemini
{
	[Serializable]
	public class GeminiMessage
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public Role role;
		public GeminiResponseMessagePart[] parts;

		public static GeminiMessage CreateUserResponse(string contentToAsk)
		{
			return new GeminiMessage()
			{
				role = Role.user,
				parts = new GeminiResponseMessagePart[]
				{
					new GeminiResponseMessagePart()
					{
						text = contentToAsk.Trim()
					}
				}
			};
		}
	}



	[Serializable]
	public class GeminiResponseMessagePart
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)] public string text = null;
		[XmlIgnore][JsonProperty(NullValueHandling = NullValueHandling.Ignore)] public GeminiResponseFunctionCall functionCall = null;
	}

	[Serializable]
	public class GeminiResponseFunctionCall
	{
		public string name = "";
		[XmlIgnore] public JToken args = null;
	}


	public enum Role
	{
		user,
		model
	}
}
