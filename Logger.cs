using System;
using System.Diagnostics;
using System.IO;

namespace BasicTwitchSoundPlayer
{
	public static class Logger
	{
		private static string FILE = "Log.txt";

		public static void AddLine(string text)
		{
#if DEBUG
			Debug.WriteLine("[LOGGER]: " + text);
#endif
			File.AppendAllText(FILE, $"{DateTime.Now}: {text}\n");
		}
	}
}
