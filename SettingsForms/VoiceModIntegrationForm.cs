using System;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SettingsForms
{
	public partial class VoiceModIntegrationForm : Form
	{
		public string Address { get; set; }
		public string API_KEY { get; set; }

		public VoiceModIntegrationForm()
		{
			InitializeComponent();

			var conf = VoiceModConfig.GetInstance();

			this.TB_Address.DataBindings.Add("Text", conf, nameof(conf.AdressPort), false, DataSourceUpdateMode.OnPropertyChanged);
			this.TB_API_KEY.DataBindings.Add("Text", conf, nameof(conf.APIKey), false, DataSourceUpdateMode.OnPropertyChanged);

		}

		private void VoiceModIntegration_Load(object sender, EventArgs e)
		{
			ConnectionStateChanged(false);
			VoiceModHandling.GetInstance().OnConnectionStateChanged += ConnectionStateChanged;
			VoiceModHandling.GetInstance().OnListOfVoicesReceived += VoicedReceived;

		}

		private void VoiceModIntegrationForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			VoiceModHandling.GetInstance().OnConnectionStateChanged -= ConnectionStateChanged;
			VoiceModHandling.GetInstance().OnListOfVoicesReceived -= VoicedReceived;

		}

		private void ConnectionStateChanged(bool connected)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new Action(() => { ConnectionStateChanged(connected); }));
				return;
			}

			if (connected)
			{
				L_Status.Text = "Connected";
				L_Status.ForeColor = System.Drawing.Color.Green;
				B_Connect.Enabled = false;
				B_Disconnect.Enabled = true;
				B_ImportFavourites.Enabled = true;
			}
			else
			{
				L_Status.Text = "Not connected";
				L_Status.ForeColor = System.Drawing.Color.Red;
				B_Connect.Enabled = true;
				B_Disconnect.Enabled = false;
				B_ImportFavourites.Enabled = false;
			}
		}

		private void VoicedReceived()
		{
			var voices = VoiceModHandling.GetInstance().VoicesAvailable;
			var config = VoiceModConfig.GetInstance();
			for(int i=0; i< config.Rewards.Count; i++)
			{
				var reward = config.Rewards[i];
				if(!voices.TryGetValue(reward.VoiceModFriendlyName, out var _))
				{
					config.Rewards.RemoveAt(i);
					i--;
				}
			}
		}

		private void B_Connect_Click(object sender, EventArgs e)
		{
			if (!VoiceModHandling.GetInstance().ConnectedToVoiceMod)
			{
				VoiceModHandling.GetInstance().ConnectToVoiceMod();
			}
		}

		private void B_Disconnect_Click(object sender, EventArgs e)
		{
			if (VoiceModHandling.GetInstance().ConnectedToVoiceMod)
			{
				VoiceModHandling.GetInstance().Disconnect();
			}
		}

		private void B_Save_Click(object sender, EventArgs e)
		{
			VoiceModConfig.GetInstance().Save();
		}

		private void B_ImportFavourites_Click(object sender, EventArgs e)
		{
			var voices = VoiceModHandling.GetInstance().VoicesAvailable;

			var config = VoiceModConfig.GetInstance();
			foreach(var voice in voices.Values)
			{
				if(voice.IsEnabled && voice.IsFavourite)
				{
					var find = config.Rewards.Find(x => x.VoiceModFriendlyName == voice.FriendlyName);
					if(find == null)
					{
						config.Rewards.Add(new VoiceModConfig.VoiceModReward()
						{
							VoiceModFriendlyName = voice.FriendlyName,
							Enabled = voice.IsEnabled,
							RewardID = "",
							RewardCooldown = 60,
							RewardPrice = 240,
							RewardText = $"Set voice to \"{voice.FriendlyName}\""
						});
					}
				}
			}

			MessageBox.Show("Done", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
