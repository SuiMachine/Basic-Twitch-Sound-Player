namespace BasicTwitchSoundPlayer.TwitchEventSub
{
	public enum EventSubClose_Code
	{
		internal_server_error = 4000,
		client_sent_inbound_traffic = 4001,
		client_failed_pingpong = 4002,
		connection_unused = 4003,
		reconnected_grace_time_expired = 4004,
		network_timeout = 4005,
		network_error = 4006,
		invalid_reconnect = 4007
	}

	public enum EventSub_MessageType
	{
		invalid,
		session_welcome,
		session_keepalive,
		notification
	}

	public enum EventSub_ChannelPoint_RedeemStatus
	{
		unfulfilled,
		fullfield
	}
}
