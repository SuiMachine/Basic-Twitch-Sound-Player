using BasicTwitchSoundPlayer.IRC;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebSocketSharp;

namespace BasicTwitchSoundPlayer
{
	public class VoiceModHandling : IDisposable
	{
		public static bool IsConfigured()
		{
			return VoiceModConfig.GetInstance().APIKey != "";
		}

		private static VoiceModHandling Instance;

		public bool Disposed { get; private set; }

		private IRCBot iRCBot;
		private MainForm parent;

		private WebSocket client;
		string currentVoice = "";
		bool currentStatus = false;
		Dictionary<string, string> VoicesAvailable = new Dictionary<string, string>();
		System.Timers.Timer timer;

		public bool ConnectedToVoiceMod { get; private set; }

		public static VoiceModHandling GetInstance()
		{
			if(Instance == null)
			{
				Instance = new VoiceModHandling();
			}
			return Instance;
		}

		private VoiceModHandling()
		{
			this.parent = MainForm.Instance;

			Disposed = false;
			parent.ThreadSafeAddPreviewText("VoiceMod key configured - let's see if we can connect...", LineType.IrcCommand);
			ConnectToVoiceMod();
		}

		private void ConnectToVoiceMod()
		{
			try
			{
				var voiceModConf = VoiceModConfig.GetInstance();
				client = new WebSocket(voiceModConf.AdressPort);
				client.OnMessage += Client_OnMessage;
				client.OnOpen += Client_OnOpen;
				client.OnClose += Client_OnClose;
				client.Connect();

				var message = new JObject()
				{
					{ "id", "ff7d7f15-0cbf-4c44-bc31-b56e0a6c9fa6" },
					{ "action", "registerClient" },
					{ "payload", new JObject()
						{
							{ "clientKey", voiceModConf.APIKey }
						}
					}
				};

				client.Send(message.ToString());
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
						Debug.WriteLine($"Set current status to {currentStatus}");
					}
					else if (value == "voiceChangerEnabledEvent")
					{
						currentStatus = true;
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
								if (voice["favorited"].Value<bool>())
								{
									var id = voice["id"].Value<string>();
									var friendlyName = voice["friendlyName"].Value<string>();
									VoicesAvailable.Add(friendlyName, id);
								}
							}
							parent.ThreadSafeAddPreviewText($"Received voices from VoiceMod - a total of {VoicesAvailable.Count}!", LineType.IrcCommand);
							parent.ThreadSafeAddPreviewText($"Voiced available: {string.Join(", ", VoicesAvailable.Select(x => "\"" + x.Key + "\""))}", LineType.IrcCommand);
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
				}
			}
		}

		public bool SetVoice(string voice)
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
							{ "voiceID", voiceID }
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

				timer = new System.Timers.Timer(30 * 1000);
				timer.Start();
				timer.Elapsed += ReturnToDefault;
				return true;
			}
			return false;
		}

		private void ReturnToDefault(object sender, System.Timers.ElapsedEventArgs e)
		{
			SetVoice(null);
		}

		private void Client_OnOpen(object sender, EventArgs e)
		{
			parent.ThreadSafeAddPreviewText("Opened VoiceMod connection", LineType.IrcCommand);
		}

		private void Client_OnClose(object sender, CloseEventArgs e)
		{
			parent.ThreadSafeAddPreviewText("Closed VoiceMod connection", LineType.IrcCommand);
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
		}

		public bool CheckIDs(string rewardID)
		{
			var config = VoiceModConfig.GetInstance();
			foreach(var reward in config.Rewards)
			{
				if (reward.RewardID == rewardID)
					return true;
			}

			return false;
		}
	}
}
