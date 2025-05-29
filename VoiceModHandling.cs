using BasicTwitchSoundPlayer.IRC;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using WebSocketSharp;
using static SuiBot_TwitchSocket.API.EventSub.ES_ChannelPoints;

namespace BasicTwitchSoundPlayer
{
	public class VoiceModHandling : IDisposable
	{
		[DebuggerDisplay(nameof(VoiceInformation) + " - {FriendlyName}")]
		public class VoiceInformation
		{
			public string ID { get; private set; } = "";
			public string FriendlyName { get; private set; } = "";
			public bool IsFavourite { get; private set; } = false;

			public VoiceInformation(string ID, string FriendlyName, bool IsFavourite)
			{
				this.ID = ID;
				this.FriendlyName = FriendlyName;
				this.IsFavourite = IsFavourite;
			}
		}

		public Action<bool> OnConnectionStateChanged;
		public Action OnListOfVoicesReceived;
		private Task SubscribingTask;

		public static bool IsConfigured()
		{
			return VoiceModConfig.GetInstance().APIKey != "";
		}

		private static VoiceModHandling Instance;

		public bool Disposed { get; private set; }
		public bool SocketConnected { get; private set; }

		private WebSocket VoiceModSocket;
		string currentVoice = "";
		public bool CurrentActiveStatus { get; private set; } = false;
		public Dictionary<string, VoiceInformation> VoicesAvailable = new Dictionary<string, VoiceInformation>();
		System.Timers.Timer timer;
		private bool m_IsConnecting = false;
		private bool m_DisableVoiceModOnConnection = false;
		private bool m_Playing = false;
		private bool m_RedeemsPaused = false;
		private ChatBot ChatBotInstance;

		private (string request, string voiceID) awaitingBitmap;

		public bool ConnectedToVoiceMod { get; private set; }

		public static VoiceModHandling GetInstance()
		{
			if (Instance == null)
			{
				Instance = new VoiceModHandling();
			}
			return Instance;
		}

		private VoiceModHandling()
		{
			Disposed = false;
			ConnectToVoiceMod();
		}

		public void SetPauseRedeems(bool value) => m_RedeemsPaused = value;

		public void ConnectToVoiceMod()
		{
			if (ConnectedToVoiceMod || m_IsConnecting)
				return;

			try
			{
				var voiceModConfig = VoiceModConfig.GetInstance();

				VoiceModSocket = new WebSocket(voiceModConfig.AdressPort);
				VoiceModSocket.OnMessage += VoiceModSocket_OnMessage;
				VoiceModSocket.OnOpen += VoiceModSocket_OnOpen;
				VoiceModSocket.OnClose += VoiceModSocket_OnClose;
				if (IsConfigured())
				{
					m_IsConnecting = true;
					MainForm.Instance.ThreadSafeAddPreviewText("Connecting to voice mod!", LineType.VoiceMod);
					VoiceModSocket.ConnectAsync();
					MainForm.Instance.TwitchEvents.OnChannelPointsRedeem += OnChannelPointsRedeem;
				}
				else
					MainForm.Instance.ThreadSafeAddPreviewText("VoiceMod is not configured - this is OK, unless you want to use it", LineType.VoiceMod);
			}
			catch (Exception ex)
			{
				m_IsConnecting = false;
				MainForm.Instance.ThreadSafeAddPreviewText($"Failed to connect to VoiceMod: {ex}", LineType.VoiceMod);
				ConnectedToVoiceMod = false;
				Dispose();
			}
		}

		private void VoiceModSocket_OnMessage(object sender, MessageEventArgs e)
		{
			//Should have probably used enums......................
			//Oh well.

			var json = JObject.Parse(e.Data);
			var msg = json["msg"];
			if (msg != null)
			{
				var value = msg.Value<string>();
				MainForm.Instance.ThreadSafeAddPreviewText($"VoiceMod response: {value}", LineType.VoiceMod);
			}
			else
			{
				var action = json["action"];
				if (action != null)
				{
					var value = action.Value<string>();
					if (value == "registerClient")
					{
						var statusCode = json["payload"]?["status"]?["code"] ?? null;
						if (statusCode != null)
						{
							var code = statusCode.Value<int>();
							//Successfully registered
							if (code == 200)
							{
								ConnectedToVoiceMod = true;
								MainForm.Instance.ThreadSafeAddPreviewText($"Client registered in VoiceMod", LineType.VoiceMod);

								var voicedRequest = new JObject()
								{
									{ "action", "getVoices" },
									{ "id", Guid.NewGuid().ToString() },
									{ "payload", new JObject() }
								};

								VoiceModSocket.Send(voicedRequest.ToString());

								//Enforce so we get status
								m_DisableVoiceModOnConnection = true;
								var voiceStatusRequest = new JObject()
								{
									{ "action", "getVoiceChangerStatus" },
									{ "id", Guid.NewGuid().ToString() },
									{ "payload", new JObject() }
								};
								VoiceModSocket.Send(voiceStatusRequest.ToString());
							}
							else
							{
								MainForm.Instance.ThreadSafeAddPreviewText($"Failed to register VoiceMod client - response code was {code}", LineType.VoiceMod);
							}
						}
						else
						{
							MainForm.Instance.ThreadSafeAddPreviewText($"Failed to register VoiceMod client - there was no status code?", LineType.VoiceMod);
						}
					}
					else if (value == "voiceChangerDisabledEvent")
					{
						CurrentActiveStatus = false;
						if (PrivateSettings.GetInstance().Debug_mode)
							MainForm.Instance.ThreadSafeAddPreviewText($"Set current status to {CurrentActiveStatus}", LineType.VoiceMod);
						Debug.WriteLine($"Set current status to {CurrentActiveStatus}");

						m_Playing = false;
						if (timer != null && timer.Enabled)
						{
							timer.Dispose();
							timer = null;
						}
					}
					else if (value == "voiceChangerEnabledEvent")
					{
						CurrentActiveStatus = true;
						if (PrivateSettings.GetInstance().Debug_mode)
							MainForm.Instance.ThreadSafeAddPreviewText($"Set current status to {CurrentActiveStatus}", LineType.VoiceMod);
						Debug.WriteLine($"Set current status to {CurrentActiveStatus}");
					}
					else if (value == "voiceLoadedEvent")
					{
						var payload = json["payload"];
						if (payload != null)
						{
							var voiceId = payload["voiceId"];
							if (voiceId != null)
							{
								currentVoice = voiceId.Value<string>();
								if (currentVoice == "nofx")
								{
									var statusRequest = new JObject()
									{
										{ "action", "getVoiceChangerStatus" },
										{ "id", Guid.NewGuid().ToString() },
										{ "payload", new JObject() }
									};
									VoiceModSocket.Send(statusRequest.ToString());
								}
								else
								{
									MainForm.Instance.ThreadSafeAddPreviewText($"Set VoiceMod to \"{voiceId.Value<string>()}\"", LineType.VoiceMod);
								}
							}
							else
							{
								MainForm.Instance.ThreadSafeAddPreviewText($"Set VoiceMod to unknown voice?", LineType.VoiceMod);
							}
						}
						else
						{
							MainForm.Instance.ThreadSafeAddPreviewText($"Empty payload?", LineType.VoiceMod);
						}

					}
					else if (value == "backgroundEffectsEnabledEvent" || value == "hearMySelfDisabledEvent")
					{
						//Just don't do anything... unless....
					}
				}
				else if (json["actionType"] != null)
				{
					var value = json["actionType"].Value<string>();
					if (value == "toggleVoiceChanger")
					{
						var newValue = json["actionObject"]?["value"];
						if (newValue != null && newValue.Value<bool>() == true)
						{
							CurrentActiveStatus = true;
							if (m_DisableVoiceModOnConnection)
							{
								ToggleVoiceMod();
							}
						}
						else
						{
							CurrentActiveStatus = false;
						}
						m_DisableVoiceModOnConnection = false;
					}
					else if (value == "getBitmap")
					{
						var result = json["actionObject"]?["result"]?["transparent"];
						if (result != null)
						{
							var actionID = json["actionID"].Value<string>();
							if (awaitingBitmap.request == actionID)
							{
								var byteData = Convert.FromBase64String(result.ToString());
								StoreThumbnails(awaitingBitmap.voiceID, byteData);
							}
						}
						awaitingBitmap = default;
					}
					else if (value == "getVoices")
					{
						var voices = json["payload"]?["voices"];
						if (voices != null)
						{
							VoicesAvailable.Clear();
							foreach (var voice in voices)
							{
								var id = voice["id"].Value<string>();
								var friendlyName = voice["friendlyName"].Value<string>();
								var favourite = voice["favorited"].Value<bool>();

								var information = new VoiceInformation(id, friendlyName, favourite);
								if (!VoicesAvailable.TryGetValue(friendlyName, out VoiceInformation voiceInformation))
									VoicesAvailable.Add(friendlyName, information);
								else
								{
									Debug.WriteLine("Duplicate entry - skipping!");
								}
							}
							MainForm.Instance.ThreadSafeAddPreviewText($"Received voices from VoiceMod - a total of {VoicesAvailable.Count}!", LineType.VoiceMod);
							OnListOfVoicesReceived?.Invoke();
						}
						else
							MainForm.Instance.ThreadSafeAddPreviewText($"Received response to get voices, but it was empty!", LineType.VoiceMod);
					}
				}
			}
		}

		private void StoreThumbnails(string voiceID, byte[] pngBytes)
		{
			//Safer path?
			voiceID.Replace(':', '_').Replace(' ', '_');

			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			Image newImage;
			using (var ms = new MemoryStream(pngBytes))
			{
				newImage = Image.FromStream(ms);
			}


			StoreThumbnail(newImage, 112, Path.Combine(folderPath, voiceID + "_112.png"));
			StoreThumbnail(newImage, 56, Path.Combine(folderPath, voiceID + "_56.png"));
			StoreThumbnail(newImage, 28, Path.Combine(folderPath, voiceID + "_28.png"));
		}

		private void StoreThumbnail(Image sourceImage, int size, string path)
		{
			var destRect = new Rectangle(0, 0, size, size);
			var destImage = new Bitmap(size, size);

			destImage.SetResolution(size, size);

			using (var graphics = Graphics.FromImage(destImage))
			{
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				using (var wrapMode = new ImageAttributes())
				{
					wrapMode.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(sourceImage, destRect, 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel, wrapMode);
				}
			}

			if (File.Exists(path))
				File.Delete(path);
			destImage.Save(path);
			destImage.Dispose();
		}

		private void ToggleVoiceMod()
		{
			var enableRequest = new JObject()
			{
				{ "action", "toggleVoiceChanger" },
				{ "id", Guid.NewGuid().ToString() },
				{ "payload", new JObject() }
			};
			VoiceModSocket.Send(enableRequest.ToString());
		}

		public bool SetVoice(string voice, float length)
		{
			if (Disposed)
				return false;
			if (voice == null || voice == "nofx")
			{
				if (CurrentActiveStatus)
				{
					ToggleVoiceMod();
				}

				currentVoice = "nofx";
				if (timer != null)
					timer.Stop();
			}
			else if (currentVoice != voice && VoicesAvailable.TryGetValue(voice, out var voiceID))
			{
				var message = new JObject()
				{
					{ "action", "loadVoice" },
					{ "id", Guid.NewGuid().ToString() },
					{ "payload", new JObject()
						{
							{ "voiceID", voiceID.ID }
						}
					}
				};

				currentVoice = voice;
				VoiceModSocket.Send(message.ToString());
				Task.Run(() => RunBackupCheck());
				if (timer != null)
				{
					timer.Stop();
					timer.Dispose();
				}

				timer = new System.Timers.Timer(length * 1000);
				timer.AutoReset = false;
				timer.Start();
				m_Playing = true;

				timer.Elapsed += ReturnToDefault;
				return true;
			}
			return false;
		}

		private async Task RunBackupCheck()
		{
			//Because VoiceMod V3 seems to occasionally set a voice, but not actually activate it
			await Task.Delay(500);
			if (ConnectedToVoiceMod && !CurrentActiveStatus)
			{
				MainForm.Instance.ThreadSafeAddPreviewText("Toggled voice using backup ?", LineType.TwitchSocketCommand);
				ToggleVoiceMod();
			}
		}

		private void ReturnToDefault(object sender, System.Timers.ElapsedEventArgs e)
		{
			m_Playing = false;
			SetVoice(null, 0);
		}

		private void VoiceModSocket_OnOpen(object sender, EventArgs e)
		{
			MainForm.Instance.ThreadSafeAddPreviewText("Opened VoiceMod connection", LineType.TwitchSocketCommand);
			ConnectedToVoiceMod = true;
			m_IsConnecting = false;
			OnConnectionStateChanged?.Invoke(true);

			var message = new JObject()
			{
				{ "id", "ff7d7f15-0cbf-4c44-bc31-b56e0a6c9fa6" },
				{ "action", "registerClient" },
				{ "payload", new JObject()
					{
						{ "clientKey", VoiceModConfig.GetInstance().APIKey }
					}
				}
			};

			VoiceModSocket.Send(message.ToString());
		}

		private void VoiceModSocket_OnClose(object sender, CloseEventArgs e)
		{
			MainForm.Instance.ThreadSafeAddPreviewText("Closed VoiceMod connection", LineType.TwitchSocketCommand);
			ConnectedToVoiceMod = false;
		}

		public void Dispose()
		{
			if (!this.Disposed)
			{
				VoiceModSocket.Close();
				VoiceModSocket.OnMessage -= VoiceModSocket_OnMessage;
				VoiceModSocket.OnOpen -= VoiceModSocket_OnOpen;
				VoiceModSocket.OnClose -= VoiceModSocket_OnClose;

				this.Disposed = true;
			}
		}

		public void Disconnect()
		{
			if (SubscribingTask != null)
				SubscribingTask.Dispose();

			if (VoiceModSocket != null)
			{
				VoiceModSocket.Close();
			}

/*			if (MainForm.TwitchSocket != null)
				MainForm.TwitchSocket.OnChannelPointsRedeem -= OnChannelPointsRedeem;*/
		}

		public void OnChannelPointsRedeem(ES_ChannelPointRedeemRequest redeem)
		{
			if (redeem.state != RedemptionStates.UNFULFILLED)
				return;

			var reward = VoiceModConfig.GetInstance().GetReward(redeem.reward.id);
			if (reward != null)
			{
				if (redeem.state == RedemptionStates.UNFULFILLED)
				{
					if (m_Playing || m_RedeemsPaused)
					{
						//MainForm.TwitchSocket?.UpdateRedemptionStatus(redeem, RedemptionStates.CANCELED);
					}
					else
					{
						if (SetVoice(reward.VoiceModFriendlyName, reward.RewardDuration))
						{
							Debug.WriteLine("This is ok");
						}

						//MainForm.TwitchSocket?.UpdateRedemptionStatus(redeem, RedemptionStates.FULFILLED);
					}
				}
			}
		}

		internal async Task DownloadImages()
		{
			var voices = VoiceModConfig.GetInstance().Rewards.ToArray();
			foreach (var voice in voices)
			{
				if (VoicesAvailable.TryGetValue(voice.VoiceModFriendlyName, out var voiceInfo))
				{
					var guid = Guid.NewGuid().ToString();
					var message = new JObject()
					{
						{ "id", guid },
						{ "action", "getBitmap" },
						{ "payload", new JObject()
							{
								{ "voiceID", voiceInfo.ID }
							}
						}
					};
					awaitingBitmap = (guid, voiceInfo.FriendlyName);

					VoiceModSocket.Send(message.ToString());
					while (awaitingBitmap != default)
						await Task.Delay(100);
				}
			}
			Process.Start(Path.Combine(Directory.GetCurrentDirectory(), "Images"));
		}
	}
}
