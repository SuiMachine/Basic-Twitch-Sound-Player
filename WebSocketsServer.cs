﻿using System;
using System.Diagnostics;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace BasicTwitchSoundPlayer
{
	public class WebSocketsListener
	{
		#region End Points logic
		public class AdjustVolume : WebSocketBehavior
		{
			protected override void OnMessage(MessageEventArgs e)
			{
				if (int.TryParse(e.Data, out var change))
				{
					var val = Mathf.Clamp(MainForm.Instance.GetVolume() + change, 0, 100);
					MainForm.Instance.SetVolume(val);
				}
				else
					MainForm.Instance.ThreadSafeAddPreviewText("Failed to parse", LineType.WebSocket);
			}
		}

		public class SetRewardsStatus : WebSocketBehavior
		{
			protected override void OnMessage(MessageEventArgs e)
			{
				if (e.Data.ToLower() == "enabled")
				{
					Debug.WriteLine("Setting redeems to false");
				}
				else if (e.Data.ToLower() == "disabled")
				{
					Debug.WriteLine("Setting redeems to true");
				}
			}
		}

		public class SetVolume : WebSocketBehavior
		{
			protected override void OnMessage(MessageEventArgs e)
			{
				if (int.TryParse(e.Data, out var change))
					MainForm.Instance.SetVolume(change);
				else
					MainForm.Instance.ThreadSafeAddPreviewText("Failed to parse", LineType.WebSocket);
			}
		}
		#endregion

		private WebSocketServer m_server;

		public void Start()
		{
			if (m_server == null)
			{
				try
				{
					MainForm.Instance.ThreadSafeAddPreviewText("Starting web server", LineType.WebSocket);
					m_server = new WebSocketServer(PrivateSettings.GetInstance().WebSocketsServerPort);
					m_server.AddWebSocketService<AdjustVolume>("/AdjustVolume");
					m_server.AddWebSocketService<SetVolume>("/SetVolume");
					m_server.AddWebSocketService<SetRewardsStatus>("/SetRewards");
					m_server.Start();
					MainForm.Instance.ThreadSafeAddPreviewText($"WebSocket started - listening on {m_server.Address}:{m_server.Port}", LineType.WebSocket);

				}
				catch (Exception e)
				{
					MainForm.Instance.ThreadSafeAddPreviewText($"Failed to open WebSocket server - {e.Message}", LineType.WebSocket);
					return;
				}
			}
		}

		public void Stop()
		{
			if (m_server != null)
			{
				m_server.Stop(CloseStatusCode.Normal, "Intentional shutdown");
				System.Threading.Thread.Sleep(200);
				m_server = null;
			}
		}
	}
}
