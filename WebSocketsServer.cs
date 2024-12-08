using System;
using System.Diagnostics;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace BasicTwitchSoundPlayer
{
	public class WebSocketsListener
	{
		public class SetRewardsStatus : WebSocketBehavior
		{
			protected override void OnMessage(MessageEventArgs e)
			{
				if(e.Data.ToLower() == "false")
				{
					Debug.WriteLine("Setting redeems to false");
				}
				else
				{
					Debug.WriteLine("Setting redeems to true");
				}
			}
		}

		private WebSocketServer m_server;

		public void Start()
		{
			if(m_server == null)
			{
				try
				{
					MainForm.Instance.ThreadSafeAddPreviewText("Starting web server", LineType.WebSocket);
					m_server = new WebSocketServer(PrivateSettings.GetInstance().WebSocketsServerPort);
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

		private void SpinThread(object obj)
		{
/*			try
			{
				cancellationToken = new CancellationTokenSource();
				MainForm.Instance.ThreadSafeAddPreviewText("Starting web server", LineType.WebSocket);
				m_server = new TcpListener(IPAddress.Parse("127.0.0.1"), PrivateSettings.GetInstance().WebSocketsServerPort);
				m_server.Start();
				MainForm.Instance.ThreadSafeAddPreviewText($"WebSocket started - listening on port {PrivateSettings.GetInstance().WebSocketsServerPort}", LineType.WebSocket);
			}
			catch (Exception e)
			{
				MainForm.Instance.ThreadSafeAddPreviewText($"Failed to open WebSocket server - {e.Message}", LineType.WebSocket);
				m_server = null;
				cancellationToken = null;
				return;
			}

			TcpClient client = m_server.AcceptTcpClient();
			NetworkStream stream = client.GetStream();
			while (true)
			{
				while (!stream.DataAvailable)
				{
					if (cancellationToken.IsCancellationRequested)
						return;
					Thread.Sleep(250);
				}

				byte[] bytes = new byte[client.Available];
				stream.Read(bytes, 0, bytes.Length);
				var str = BitConverter.ToString(bytes);

			}*/
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
