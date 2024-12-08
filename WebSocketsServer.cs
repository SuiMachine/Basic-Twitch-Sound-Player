using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BasicTwitchSoundPlayer
{
	public class WebSocketsServer
	{
		private TcpListener m_server;

		public void Start()
		{
			m_server = new TcpListener(IPAddress.Parse("127.0.0.1"), PrivateSettings.GetInstance().WebSocketsServer);
		}
	}
}
