using BasicTwitchSoundPlayer.IRC;
using System;
using System.Threading.Tasks;
using TwitchLib.PubSub;
using static BasicTwitchSoundPlayer.IRC.KrakenConnections;

namespace BasicTwitchSoundPlayer
{
	public class TwitchSocket
	{
		private IRCBot iRCBot;
		private Task SubscribingTask;
		private TwitchPubSub TwitchPubSubClient;

		public Action<ChannelPointRedeemRequest> OnChannelPointsRedeem;

		public async Task CreateSessionAndSocket()
		{
			while (!iRCBot.BotRunning || iRCBot.irc == null || !iRCBot.irc.ConnectedStatus)
				await Task.Delay(2500);
			var rewards = await iRCBot.irc.krakenConnection.GetRewardsList();

			TwitchPubSubClient = new TwitchPubSub();
			TwitchPubSubClient.OnPubSubServiceConnected += TwitchPubSubClient_OnPubSubServiceConnected;
			TwitchPubSubClient.OnListenResponse += TwitchPubSubClient_OnListenResponse;
			TwitchPubSubClient.OnChannelPointsRewardRedeemed += TwitchPubSubClient_OnChannelPointsRewardRedeemed;

			TwitchPubSubClient.ListenToChannelPoints(iRCBot.irc.krakenConnection.BroadcasterID);
			TwitchPubSubClient.Connect();
		}

		private void TwitchPubSubClient_OnChannelPointsRewardRedeemed(object sender, TwitchLib.PubSub.Events.OnChannelPointsRewardRedeemedArgs e)
		{
			if (!Enum.TryParse(e.RewardRedeemed.Redemption.Status, true, out KrakenConnections.RedemptionStates state))
				return;

			OnChannelPointsRedeem?.Invoke(new ChannelPointRedeemRequest(e.RewardRedeemed.Redemption.User.Id, e.RewardRedeemed.Redemption.Reward.Id, e.RewardRedeemed.Redemption.Id, state));
		}

		private void TwitchPubSubClient_OnPubSubServiceConnected(object sender, EventArgs e)
		{
			var auth = "oauth:" + PrivateSettings.GetInstance().TwitchPassword;
			TwitchPubSubClient.SendTopics(oauth: auth);
		}

		private void TwitchPubSubClient_OnListenResponse(object sender, TwitchLib.PubSub.Events.OnListenResponseArgs e)
		{
			if (!e.Successful)
				throw new Exception($"Failed to listen! Response: {e.Response}");
		}

		public void SetIrcReference(IRCBot iRCBot)
		{
			this.iRCBot = iRCBot;

			SubscribingTask = Task.Run(CreateSessionAndSocket);
		}

		public void UpdateRedemptionStatus(ChannelPointRedeemRequest redeem, KrakenConnections.RedemptionStates status)
		{
			if (redeem.state != RedemptionStates.UNFULFILLED)
			{
				MainForm.Instance.ThreadSafeAddPreviewText("Can't change the state of already accepted/rejected redeem - this needs to be fixed!", LineType.IrcCommand);
				return;
			}

			if (status == RedemptionStates.UNFULFILLED)
			{
				MainForm.Instance.ThreadSafeAddPreviewText("Can't set state to UNFULFILLED - this needs to be fixed!", LineType.IrcCommand);
				return;
			}

			redeem.state = status;
			iRCBot.irc.krakenConnection.UpdateRedemptionStatus(redeem.rewardId, new string[]
			{
				redeem.redemptionId,
			}, status);
		}
	}
}
