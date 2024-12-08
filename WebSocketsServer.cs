using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace BasicTwitchSoundPlayer
{
	public class WebSocketsServer
	{
		private TcpListener m_server;
		private Thread m_serverThread;

		public void Start()
		{
			if (m_serverThread == null)
			{
				m_serverThread = new Thread(new ThreadStart(() =>
				{
					SpinThread("127.0.0.1", PrivateSettings.GetInstance().WebSocketsServerPort);
				}));
				m_serverThread.Start();
			}
		}

		private void SpinThread(string ip, int port)
		{
			try
			{
				MainForm.Instance.ThreadSafeAddPreviewText("Starting web server", LineType.WebSocket);
				m_server = new TcpListener(IPAddress.Parse(ip), port);
				m_server.Start();
				MainForm.Instance.ThreadSafeAddPreviewText($"WebSocket started - listening on port {PrivateSettings.GetInstance().WebSocketsServerPort}", LineType.WebSocket);
			}
			catch (Exception e)
			{
				MainForm.Instance.ThreadSafeAddPreviewText($"Failed to open WebSocket server - {e.Message}", LineType.WebSocket);
				m_server = null;
				m_serverThread = null;
				return;
			}


			TcpClient client = m_server.AcceptTcpClient();
			NetworkStream stream = client.GetStream();
			while(true)
			{
				while(!stream.DataAvailable)
				{
					Thread.Sleep(250);
				}

				byte[] bytes = new byte[client.Available];
				stream.Read(bytes, 0, bytes.Length);
				var str = BitConverter.ToString(bytes);

			}
		}

		public void Stop()
		{
			if (m_serverThread != null)
				m_serverThread.Abort();
			if (m_server != null)
				m_server.Stop();

			m_server = null;
		}
	}
}
