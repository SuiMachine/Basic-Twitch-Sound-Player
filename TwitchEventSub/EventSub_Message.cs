using Newtonsoft.Json.Linq;
using System;

namespace BasicTwitchSoundPlayer.TwitchEventSub
{
	[Serializable]
	public class EventSub_Message
	{
		public EventSub_Metadata metadata;
		public JToken payload;
	}
}
