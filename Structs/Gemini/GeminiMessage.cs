using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BasicTwitchSoundPlayer.Structs.Gemini
{
	[Serializable]
	public class GeminiMessage
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public Role role;
		public GeminiMessagePart[] parts;

		public static GeminiMessage CreateUserResponse(string contentToAsk)
		{
			return new GeminiMessage()
			{
				role = Role.user,
				parts = new GeminiMessagePart[]
				{
					new GeminiMessagePart()
					{
						text = contentToAsk.Trim()
					}
				}
			};
		}
	}

	[Serializable]
	public class GeminiMessagePart
	{
		public string text;
	}

	public enum Role
	{
		user,
		model
	}
}
