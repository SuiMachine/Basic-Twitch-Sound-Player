using BasicTwitchSoundPlayer.Interfaces;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static BasicTwitchSoundPlayer.VoiceModConfig;

namespace BasicTwitchSoundPlayer.SettingsForms
{
	public partial class VoiceModIntegrationForm : Form
	{
		public string Address { get; set; }
		public string API_KEY { get; set; }
		protected BindingList<IVoiceModeRewardBindingInterface> VoiceModRewardBinding { get; set; }


		#region Grid_Constants
		int COLUMNINDEX_VOICENAME = 0;
		int COLUMNINDEX_REWARD_ID = 1;
		int COLUMNINDEX_REWARD_PRICE = 2;
		int COLUMNINDEX_REWARD_COOLDOWN = 3;
		int COLUMNINDEX_ENABLED = 4;
		int COLUMNINDEX_REWARD_TEXT = 5;
		#endregion


		public VoiceModIntegrationForm()
		{
			InitializeComponent();

			var conf = VoiceModConfig.GetInstance();

			CreateVoiceModBinding();

			this.TB_Address.DataBindings.Add("Text", conf, nameof(conf.AdressPort), false, DataSourceUpdateMode.OnPropertyChanged);
			this.TB_API_KEY.DataBindings.Add("Text", conf, nameof(conf.APIKey), false, DataSourceUpdateMode.OnPropertyChanged);

		}

		private void CreateVoiceModBinding()
		{
			VoicesDataGrid.Columns.Clear();
			VoiceModRewardBinding = new BindingList<IVoiceModeRewardBindingInterface>(VoiceModConfig.GetInstance().Rewards.Cast<IVoiceModeRewardBindingInterface>().ToList()) { AllowNew = true, AllowRemove = true };
			VoicesDataGrid.AutoGenerateColumns = false;
			VoicesDataGrid.AutoSize = true;
			VoicesDataGrid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
			VoicesDataGrid.DataSource = VoiceModRewardBinding;

			VoicesDataGrid.CellFormatting += VoicesDataGrid_CellFormatting;


			var nameColumn = new DataGridViewTextBoxColumn();
			nameColumn.Name = "Voice Name";
			nameColumn.Width = 350;
			nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			nameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			VoicesDataGrid.Columns.Add(nameColumn);

			var rewardIDColumn = new DataGridViewTextBoxColumn();
			rewardIDColumn.Name = "Reward ID";
			rewardIDColumn.Width = 350;
			rewardIDColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			rewardIDColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			VoicesDataGrid.Columns.Add(rewardIDColumn);

			var RewardPrice = new DataGridViewTextBoxColumn();
			RewardPrice.Name = "Price";
			RewardPrice.Width = 32;
			RewardPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			RewardPrice.SortMode = DataGridViewColumnSortMode.NotSortable;
			VoicesDataGrid.Columns.Add(RewardPrice);

			var RewardCooldown = new DataGridViewTextBoxColumn();
			RewardCooldown.Name = "Cooldown";
			RewardCooldown.Width = 32;
			RewardCooldown.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			RewardCooldown.SortMode = DataGridViewColumnSortMode.NotSortable;
			VoicesDataGrid.Columns.Add(RewardCooldown);

			var EnabledField = new DataGridViewTextBoxColumn();
			EnabledField.Name = "Enabled";
			EnabledField.Width = 32;
			EnabledField.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			EnabledField.SortMode = DataGridViewColumnSortMode.NotSortable;
			VoicesDataGrid.Columns.Add(EnabledField);

			var RewardText = new DataGridViewTextBoxColumn();
			RewardText.Name = "Reward text";
			RewardText.Width = 350;
			RewardText.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			RewardText.SortMode = DataGridViewColumnSortMode.NotSortable;
			VoicesDataGrid.Columns.Add(RewardText);
		}

		private void VoicesDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			var config = VoiceModConfig.GetInstance();
			if(e.RowIndex < config.Rewards.Count)
			{
				if(e.ColumnIndex == COLUMNINDEX_VOICENAME)
				{
					e.Value = config.Rewards[e.RowIndex].VoiceModFriendlyName;
				}
				else if (e.ColumnIndex == COLUMNINDEX_REWARD_ID)
				{
					e.Value = config.Rewards[e.RowIndex].RewardID;
				}
				else if (e.ColumnIndex == COLUMNINDEX_REWARD_PRICE)
				{
					e.Value = config.Rewards[e.RowIndex].RewardPrice;
				}
				else if (e.ColumnIndex == COLUMNINDEX_REWARD_COOLDOWN)
				{
					e.Value = config.Rewards[e.RowIndex].RewardCooldown;
				}
				else if (e.ColumnIndex == COLUMNINDEX_ENABLED)
				{
					e.Value = config.Rewards[e.RowIndex].Enabled;
				}
				else if (e.ColumnIndex == COLUMNINDEX_REWARD_TEXT)
				{
					e.Value = config.Rewards[e.RowIndex].RewardText;
				}
			}
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

			VoicesDataGrid.CellFormatting -= VoicesDataGrid_CellFormatting;


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
