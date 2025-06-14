namespace SSC.MixItUpBridge
{
	public abstract class MixItUpWebHookRequests_Base
	{
		public abstract string Type { get; }
	}

	public class MixItUpWebHookRequests_SoundRewardsStatus : MixItUpWebHookRequests_Base
	{
		public override string Type => "SoundRewardStatus";
		public bool SoundRewards;
		public bool VoiceRewards;
	}

	public class MixItUpWebHookRequests_ChannelGoalReached : MixItUpWebHookRequests_Base
	{
		public override string Type => "ChannelGoalReached";
	}
}
