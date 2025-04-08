using Newtonsoft.Json.Converters;
using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace BasicTwitchSoundPlayer.TwitchEventSub
{
	[DebuggerDisplay("EventSub_Metadata - {message_type} ({message_timestamp}) - {message_id}")]
	[Serializable]
	public class EventSub_Metadata
	{
		public string message_id;
		[JsonConverter(typeof(StringEnumConverter))] public EventSub_MessageType message_type;
		public DateTime message_timestamp;
		public string subscription_type = null;
		public int? subscription_version = null;
	}
}
