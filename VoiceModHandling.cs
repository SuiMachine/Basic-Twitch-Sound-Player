using BasicTwitchSoundPlayer.IRC;
using BasicTwitchSoundPlayer.Structs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Interop;
using TwitchLib.PubSub;
using TwitchLib.PubSub.Models.Responses.Messages.Redemption;
using WebSocketSharp;

namespace BasicTwitchSoundPlayer
{
	public class VoiceModHandling : IDisposable
	{
		public class VoiceInformation
		{
			public string ID { get; private set; } = "";
			public string FriendlyName { get; private set; } = "";
			public bool IsFavourite { get; private set; } = false;
			public bool IsEnabled { get; private set; } = false;

			public VoiceInformation(string ID, string FriendlyName, string BitmapCheckSum, bool IsFavourite, bool IsEnabled)
			{
				this.ID = ID;
				this.FriendlyName = FriendlyName;
				this.IsFavourite = IsFavourite;
				this.IsEnabled = IsEnabled;
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

		private IRCBot iRCBot;
		private MainForm parent;

		private WebSocket client;
		string currentVoice = "";
		bool currentStatus = false;
		public Dictionary<string, VoiceInformation> VoicesAvailable = new Dictionary<string, VoiceInformation>();
		System.Timers.Timer timer;
		private bool Playing = false;
		private TwitchPubSub TwitchPubSubClient { get; set; }
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
			this.parent = MainForm.Instance;

			Disposed = false;
			ConnectToVoiceMod();
		}

		public void ConnectToVoiceMod()
		{
			if (ConnectedToVoiceMod)
				return;

			try
			{
				var voiceModConf = VoiceModConfig.GetInstance();

				client = new WebSocket(voiceModConf.AdressPort);
				client.OnMessage += Client_OnMessage;
				client.OnOpen += Client_OnOpen;
				client.OnClose += Client_OnClose;
				if (IsConfigured())
				{
					parent.ThreadSafeAddPreviewText("Connecting to voice mod!", LineType.IrcCommand);
					client.ConnectAsync();
				}
				else
					parent.ThreadSafeAddPreviewText("VoiceMod is not configured - this is OK, unless you want to use it", LineType.IrcCommand);
			}
			catch (Exception ex)
			{
				parent.ThreadSafeAddPreviewText($"Failed to connect to voicemod: {ex}", LineType.IrcCommand);
				ConnectedToVoiceMod = false;
				Dispose();
			}
		}

		private void Client_OnMessage(object sender, MessageEventArgs e)
		{
			//Should have probably used enums......................
			//Oh well.

			var json = JObject.Parse(e.Data);
			var msg = json["msg"];
			if (msg != null)
			{
				var value = msg.Value<string>();
				parent.ThreadSafeAddPreviewText($"VoiceMod response: {value}", LineType.IrcCommand);
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
								parent.ThreadSafeAddPreviewText($"Client registered in VoiceMod", LineType.IrcCommand);

								var voicedRequest = new JObject()
								{
									{ "action", "getVoices" },
									{ "id", Guid.NewGuid().ToString() },
									{ "payload", new JObject() }
								};

								client.Send(voicedRequest.ToString());

								var message = new JObject()
								{
									{ "action", "loadVoice" },
									{ "id", Guid.NewGuid().ToString() },
									{ "payload", new JObject()
										{
											{ "voiceID", "nofx" }
										}
									}
								};

								client.Send(message.ToString());
							}
							else
							{
								parent.ThreadSafeAddPreviewText($"Failed to register VoiceMod client - response code was {code}", LineType.IrcCommand);
							}
						}
						else
						{
							parent.ThreadSafeAddPreviewText($"Failed to register VoiceMod client - there was no status code?", LineType.IrcCommand);
						}
					}
					else if (value == "voiceChangerDisabledEvent")
					{
						currentStatus = false;
						if (PrivateSettings.GetInstance().Debug_mode)
							parent.ThreadSafeAddPreviewText($"Set current status to {currentStatus}", LineType.IrcCommand);
						Debug.WriteLine($"Set current status to {currentStatus}");

						Playing = false;
						if(timer != null && timer.Enabled)
						{
							timer.Dispose();
							timer = null;
						}
					}
					else if (value == "voiceChangerEnabledEvent")
					{
						currentStatus = true;
						if (PrivateSettings.GetInstance().Debug_mode)
							parent.ThreadSafeAddPreviewText($"Set current status to {currentStatus}", LineType.IrcCommand);
						Debug.WriteLine($"Set current status to {currentStatus}");
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
									client.Send(statusRequest.ToString());
								}
								else
								{
									parent.ThreadSafeAddPreviewText($"Set VoiceMod to \"{voiceId.Value<string>()}\"", LineType.IrcCommand);
								}
							}
							else
							{
								parent.ThreadSafeAddPreviewText($"Set VoiceMod to unknown voice?", LineType.IrcCommand);
							}
						}
						else
						{
							parent.ThreadSafeAddPreviewText($"Empty payload?", LineType.IrcCommand);
						}

					}
					else if (value == "backgroundEffectsEnabledEvent" || value == "hearMySelfDisabledEvent")
					{
						//Just don't do anything... unless....
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
								var checksum = voice["bitmapChecksum"].Value<string>();
								var enabled = voice["isEnabled"].Value<bool>();

								var information = new VoiceInformation(id, friendlyName, checksum, favourite, enabled);
								if (!VoicesAvailable.ContainsKey(friendlyName))
									VoicesAvailable.Add(friendlyName, information);
							}
							parent.ThreadSafeAddPreviewText($"Received voices from VoiceMod - a total of {VoicesAvailable.Count}!", LineType.IrcCommand);
							OnListOfVoicesReceived?.Invoke();
						}
						else
							parent.ThreadSafeAddPreviewText($"Received response to get voices, but it was empty!", LineType.IrcCommand);
					}
				}
				else if (json["actionType"] != null)
				{
					var value = json["actionType"].Value<string>();
					if (value == "toggleVoiceChanger")
					{
						var newValue = json["actionObject"]?["value"];
						if (newValue != null && newValue.Value<bool>() == true && currentVoice == "nofx")
						{
							var disableRequest = new JObject()
							{
								{ "action", "toggleVoiceChanger" },
								{ "id", Guid.NewGuid().ToString() },
								{ "payload", new JObject() }
							};
							client.Send(disableRequest.ToString());
						}
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

		public bool SetVoice(string voice, float lenght)
		{
			if (Disposed)
				return false;
			if (voice == null || voice == "nofx")
			{
				if (currentStatus)
				{
					var enableRequest = new JObject()
					{
							{ "action", "toggleVoiceChanger" },
							{ "id", Guid.NewGuid().ToString() },
							{ "payload", new JObject() }
					};
					client.Send(enableRequest.ToString());
				}
				var message = new JObject()
				{
					{ "action", "loadVoice" },
					{ "id", Guid.NewGuid().ToString() },
					{ "payload", new JObject()
						{
							{ "voiceID", "nofx" }
						}
					}
				};
				client.Send(message.ToString());

				currentVoice = "nofx";
				if (timer != null)
					timer.Stop();
			}
			else if (currentVoice != voice && VoicesAvailable.TryGetValue(voice, out var voiceID))
			{
				if (!currentStatus)
				{
					var enableRequest = new JObject()
					{
							{ "action", "toggleVoiceChanger" },
							{ "id", Guid.NewGuid().ToString() },
							{ "payload", new JObject() }
					};
					client.Send(enableRequest.ToString());
				}

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
				client.Send(message.ToString());
				if (timer != null)
				{
					timer.Stop();
					timer.Dispose();
				}

				timer = new System.Timers.Timer(lenght * 1000);
				timer.Start();
				Playing = true;
				timer.Elapsed += ReturnToDefault;
				return true;
			}
			return false;
		}

		private void ReturnToDefault(object sender, System.Timers.ElapsedEventArgs e)
		{
			Playing = false;
			SetVoice(null, 0);
		}

		private void Client_OnOpen(object sender, EventArgs e)
		{
			parent.ThreadSafeAddPreviewText("Opened VoiceMod connection", LineType.IrcCommand);
			ConnectedToVoiceMod = true;
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

			client.Send(message.ToString());
		}

		private void Client_OnClose(object sender, CloseEventArgs e)
		{
			parent.ThreadSafeAddPreviewText("Closed VoiceMod connection", LineType.IrcCommand);
			ConnectedToVoiceMod = false;
		}

		public void Dispose()
		{
			if (!this.Disposed)
			{
				client.Close();
				client.OnMessage -= Client_OnMessage;
				client.OnOpen -= Client_OnOpen;
				client.OnClose -= Client_OnClose;

				this.Disposed = true;
			}
		}

		public void SetIrcReference(IRCBot iRCBot)
		{
			this.iRCBot = iRCBot;

			SubscribingTask = Task.Run(CreateSessionAndSocket);
		}

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
			var reward = VoiceModHandling.GetInstance().CheckIDs(e.RewardRedeemed.Redemption.Reward.Id);
			if (reward != null)
			{
				if (e.RewardRedeemed.Redemption.Status == "UNFULFILLED")
				{
					if (Playing)
					{
						iRCBot.irc.krakenConnection.UpdateRedemptionStatus(e.RewardRedeemed.Redemption.Reward.Id, new string[]
						{
							e.RewardRedeemed.Redemption.Id,
						}, KrakenConnections.RedemptionStates.CANCELED);
					}
					else
					{
						SetVoice(reward.VoiceModFriendlyName, reward.RewardDuration);
						iRCBot.irc.krakenConnection.UpdateRedemptionStatus(e.RewardRedeemed.Redemption.Reward.Id, new string[]
						{
							e.RewardRedeemed.Redemption.Id,
						}, KrakenConnections.RedemptionStates.FULFILLED);
					}
				}
			}
		}

		private void TwitchPubSubClient_OnListenResponse(object sender, TwitchLib.PubSub.Events.OnListenResponseArgs e)
		{
			if (!e.Successful)
				throw new Exception($"Failed to listen! Response: {e.Response}");
		}

		private void TwitchPubSubClient_OnPubSubServiceConnected(object sender, EventArgs e)
		{
			var auth = "oauth:" + PrivateSettings.GetInstance().TwitchPassword;
			TwitchPubSubClient.SendTopics(oauth: auth);
		}

		public VoiceModConfig.VoiceModReward CheckIDs(string rewardID)
		{
			var config = VoiceModConfig.GetInstance();
			foreach (var reward in config.Rewards)
			{
				if (reward.RewardID == rewardID)
					return reward;
			}

			return null;
		}

		public void Disconnect()
		{
			if (SubscribingTask != null)
				SubscribingTask.Dispose();

			if (client != null)
			{
				client.Close();
			}

			TwitchPubSubClient.OnPubSubServiceConnected -= TwitchPubSubClient_OnPubSubServiceConnected;
			TwitchPubSubClient.OnListenResponse -= TwitchPubSubClient_OnListenResponse;
			TwitchPubSubClient.OnChannelPointsRewardRedeemed -= TwitchPubSubClient_OnChannelPointsRewardRedeemed;
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

					client.Send(message.ToString());
					while (awaitingBitmap != default)
						await Task.Delay(100);
				}
			}
			Process.Start(Path.Combine(Directory.GetCurrentDirectory(), "Images"));
		}
	}
}
