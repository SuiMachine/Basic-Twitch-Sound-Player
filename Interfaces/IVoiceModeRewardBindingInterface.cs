using System;

namespace SSC.Interfaces
{
	public interface IVoiceModeRewardBindingInterface : ICloneable
	{
		string VoiceModFriendlyName { get; set; }
		string RewardID { get; set; }
		int RewardCost { get; set; }
		int RewardCooldown { get; set; }
		bool Enabled { get; set; }
		string RewardDescription { get; set; }
	}
}
