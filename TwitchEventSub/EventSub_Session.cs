using Newtonsoft.Json.Converters;
using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace BasicTwitchSoundPlayer.TwitchEventSub
{
	[DebuggerDisplay(nameof(EventSub_Session) + " {status}")]
	[Serializable]
	public class EventSub_Session
	{
		public enum SessionStatus
		{
			invalid,
			connected
		}

		public string id = "";
		[JsonConverter(typeof(StringEnumConverter))] public SessionStatus status = SessionStatus.invalid;
		public DateTime connected_at = DateTime.MinValue;
		public int keepalive_timeout_seconds = 10;
		public string reconnect_url = null;
		public string recovery_url = null;
	}
}
