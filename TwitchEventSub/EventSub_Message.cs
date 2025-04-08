using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace BasicTwitchSoundPlayer.TwitchEventSub
{
	[Serializable]
	public class EventSub_Message
	{
		public EventSub_Metadata metadata;
		public JToken payload;
	}
}
