using BasicTwitchSoundPlayer.Interfaces;
using BasicTwitchSoundPlayer.SettingsForms.EditForm;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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
		enum TABLE_COLUMS
		{
			COLUMNINDEX_VOICENAME,
			COLUMNINDEX_REWARD_NAME,
			COLUMNINDEX_REWARD_PRICE,
			COLUMNINDEX_REWARD_COOLDOWN,
			COLUMNINDEX_REWARD_DURATION,
			COLUMNINDEX_ENABLED,
			COLUMNINDEX_REWARD_TEXT
		}
		#endregion


		public VoiceModIntegrationForm()
		{
			InitializeComponent();

			var conf = VoiceModConfig.GetInstance();

			CreateVoiceModBinding();

			this.TB_Address.DataBindings.Add("Text", conf, nameof(conf.AdressPort), false, DataSourceUpdateMode.OnPropertyChanged);
			this.TB_API_KEY.DataBindings.Add("Text", conf, nameof(conf.APIKey), false, DataSourceUpdateMode.OnPropertyChanged);

			VoicesDataGrid.CellFormatting += VoicesDataGrid_CellFormatting;
			VoicesDataGrid.DoubleClick += VoicesDataGrid_DoubleClick;
			VoicesDataGrid.KeyDown += VoicesDataGrid_KeyDown;
		}

		private void CreateVoiceModBinding()
		{
			VoicesDataGrid.Columns.Clear();
			VoiceModRewardBinding = new BindingList<IVoiceModeRewardBindingInterface>(VoiceModConfig.GetInstance().Rewards.Cast<IVoiceModeRewardBindingInterface>().ToList()) { AllowNew = true, AllowRemove = true };
			VoicesDataGrid.AutoGenerateColumns = false;
			VoicesDataGrid.MultiSelect = false;
			VoicesDataGrid.AutoSize = true;
			VoicesDataGrid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
			VoicesDataGrid.DataSource = VoiceModRewardBinding;



			var nameColumn = new DataGridViewTextBoxColumn();
			nameColumn.Name = "Voice Name";
			nameColumn.Width = 120;
			nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			nameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			VoicesDataGrid.Columns.Add(nameColumn);


			var rewardName = new DataGridViewTextBoxColumn();
			rewardName.Name = "Reward Name";
			rewardName.Width = 350;
			rewardName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			rewardName.SortMode = DataGridViewColumnSortMode.NotSortable;
			VoicesDataGrid.Columns.Add(rewardName);

			var RewardPrice = new DataGridViewTextBoxColumn();
			RewardPrice.Name = "Price";
			RewardPrice.Width = 60;
			RewardPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			RewardPrice.SortMode = DataGridViewColumnSortMode.NotSortable;
			VoicesDataGrid.Columns.Add(RewardPrice);

			var RewardCooldown = new DataGridViewTextBoxColumn();
			RewardCooldown.Name = "Cooldown";
			RewardCooldown.Width = 60;
			RewardCooldown.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			RewardCooldown.SortMode = DataGridViewColumnSortMode.NotSortable;
			VoicesDataGrid.Columns.Add(RewardCooldown);

			var durationColumn = new DataGridViewTextBoxColumn();
			durationColumn.Name = "Duration";
			durationColumn.Width = 80;
			durationColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			durationColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			VoicesDataGrid.Columns.Add(durationColumn);

			var EnabledField = new DataGridViewTextBoxColumn();
			EnabledField.Name = "Enabled";
			EnabledField.Width = 60;
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

		private void RegenerateDataBinding()
		{
			VoiceModRewardBinding = new BindingList<IVoiceModeRewardBindingInterface>(VoiceModConfig.GetInstance().Rewards.Cast<IVoiceModeRewardBindingInterface>().ToList()) { AllowNew = true, AllowRemove = true };
			VoicesDataGrid.DataSource = VoiceModRewardBinding;
		}

		private void VoicesDataGrid_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				var row = VoicesDataGrid.CurrentRow.Index;
				var config = VoiceModConfig.GetInstance();

				if (row >= 0 && row < config.Rewards.Count)
				{
					config.Rewards.RemoveAt(row);
					VoicesDataGrid.Invalidate();
					RegenerateDataBinding();
				}
			}
		}

		private void VoicesDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			var config = VoiceModConfig.GetInstance();
			if (e.RowIndex < config.Rewards.Count)
			{
				if (e.ColumnIndex == (int)TABLE_COLUMS.COLUMNINDEX_VOICENAME)
				{
					e.Value = config.Rewards[e.RowIndex].VoiceModFriendlyName;
				}
				else if (e.ColumnIndex == (int)TABLE_COLUMS.COLUMNINDEX_REWARD_NAME)
				{
					e.Value = config.Rewards[e.RowIndex].RewardTitle;
				}
				else if (e.ColumnIndex == (int)TABLE_COLUMS.COLUMNINDEX_REWARD_PRICE)
				{
					e.Value = config.Rewards[e.RowIndex].RewardCost;
				}
				else if (e.ColumnIndex == (int)TABLE_COLUMS.COLUMNINDEX_REWARD_COOLDOWN)
				{
					e.Value = config.Rewards[e.RowIndex].RewardCooldown;
				}
				else if (e.ColumnIndex == (int)TABLE_COLUMS.COLUMNINDEX_REWARD_DURATION)
				{
					e.Value = config.Rewards[e.RowIndex].RewardDuration;
				}
				else if (e.ColumnIndex == (int)TABLE_COLUMS.COLUMNINDEX_ENABLED)
				{
					e.Value = config.Rewards[e.RowIndex].Enabled;
				}
				else if (e.ColumnIndex == (int)TABLE_COLUMS.COLUMNINDEX_REWARD_TEXT)
				{
					e.Value = config.Rewards[e.RowIndex].RewardDescription;
				}
			}
		}

		private void VoicesDataGrid_DoubleClick(object sender, EventArgs e)
		{
			var config = VoiceModConfig.GetInstance();
			var row = VoicesDataGrid.CurrentRow.Index;
			if (row >= 0 && row < config.Rewards.Count)
			{
				var form = new EditForm.VoiceModEditForm(config.Rewards[row]);
				var result = form.ShowDialog();
				if (result == DialogResult.OK)
				{
					var sound = config.Rewards[row];
					sound.RewardTitle = form.RewardName;
					sound.RewardCooldown = form.RewardCooldown;
					sound.Enabled = form.RewardEnabled;
					sound.RewardDuration = form.RewardDuration;
					sound.RewardCost = form.RewardPrice;
					sound.RewardDescription = form.RewardText;
					VoicesDataGrid.InvalidateRow(row);
				}
			}
		}

		private void VoiceModIntegration_Load(object sender, EventArgs e)
		{
			ConnectionStateChanged(VoiceModHandling.GetInstance().ConnectedToVoiceMod);
			VoiceModHandling.GetInstance().OnConnectionStateChanged += ConnectionStateChanged;
			VoiceModHandling.GetInstance().OnListOfVoicesReceived += VoicedReceived;

		}

		private void VoiceModIntegrationForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			VoiceModHandling.GetInstance().OnConnectionStateChanged -= ConnectionStateChanged;
			VoiceModHandling.GetInstance().OnListOfVoicesReceived -= VoicedReceived;

			VoicesDataGrid.CellFormatting -= VoicesDataGrid_CellFormatting;
			VoicesDataGrid.DoubleClick -= VoicesDataGrid_DoubleClick;
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
				B_Add.Enabled = true;
				B_Images.Enabled = true;
			}
			else
			{
				L_Status.Text = "Not connected";
				L_Status.ForeColor = System.Drawing.Color.Red;
				B_Connect.Enabled = true;
				B_Disconnect.Enabled = false;
				B_ImportFavourites.Enabled = false;
				B_Add.Enabled = false;
				B_Images.Enabled = false;
			}
		}

		private void VoicedReceived()
		{
			var voices = VoiceModHandling.GetInstance().VoicesAvailable;
			var config = VoiceModConfig.GetInstance();
			for (int i = 0; i < config.Rewards.Count; i++)
			{
				var reward = config.Rewards[i];
				if (!voices.TryGetValue(reward.VoiceModFriendlyName, out var _))
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
			foreach (var voice in voices.Values)
			{
				//Skip clean voice!
				if (voice.ID == "nofx")
					continue;

				if (voice.IsEnabled && voice.IsFavourite)
				{
					var find = config.Rewards.Find(x => x.VoiceModFriendlyName == voice.FriendlyName);
					if (find == null)
					{
						var duration = 60;
						config.Rewards.Add(new VoiceModConfig.VoiceModReward()
						{
							RewardTitle = $"Set voice to \"{voice.FriendlyName}\"",
							VoiceModFriendlyName = voice.FriendlyName,
							Enabled = voice.IsEnabled,
							RewardID = "",
							RewardCooldown = 1,
							RewardDuration = duration,
							RewardCost = 50,
							RewardDescription = $"Set voice to \"{voice.FriendlyName}\" for {duration} seconds (powered by VoiceMod)"
						});
					}
				}
			}

			MessageBox.Show("Done", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
			VoicesDataGrid.Invalidate();
		}

		private async void B_CreateMissingRewards_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to do that?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.No)
				return;


			var settings = PrivateSettings.GetInstance();
			IRC.KrakenConnections apiConnection = new IRC.KrakenConnections(settings.TwitchUsername, settings.TwitchPassword);
			await apiConnection.GetBroadcasterIDAsync();

			var voices = VoiceModConfig.GetInstance();
			foreach (var voice in voices.Rewards)
			{
				if (apiConnection.CachedRewards == null)
					_ = await apiConnection.GetRewardsList();
				var resultReward = await apiConnection.CreateOrUpdateRewardVoiceModAsync(voice);
				if (resultReward != null)
				{
					voice.RewardID = resultReward.id;
					voice.IsSetup = true;
				}
				await Task.Delay(2000);
			}

			MessageBox.Show("Done!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void B_Add_Click(object sender, EventArgs e)
		{
			var form = new VoiceModAddForm();
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
			{
				var voiceAdd = form.SelectedVoice;
				var reward = new VoiceModReward()
				{
					VoiceModFriendlyName = voiceAdd.FriendlyName,
					Enabled = true,
					RewardDescription = form.Description,
					RewardCooldown = form.Cooldown,
					RewardCost = form.Price,
					RewardDuration = form.Duration,
					RewardID = voiceAdd.ID,
					RewardTitle = form.RewardName
				};
				VoiceModConfig.GetInstance().Rewards.Add(reward);
				VoicesDataGrid.Invalidate();
				VoicesDataGrid.Refresh();
				RegenerateDataBinding();
			}
		}

		private async void B_Images_ClickAsync(object sender, EventArgs e)
		{
			await VoiceModHandling.GetInstance().DownloadImages();
		}
	}
}
