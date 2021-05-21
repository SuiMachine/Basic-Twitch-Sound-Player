using System;
using System.IO;

namespace BasicTwitchSoundPlayer
{
	public static class Logger
	{
		private static string FILE = "Log.txt";

		public static void AddLine(string text)
		{
			File.AppendAllText(FILE, string.Format("{0}: {1}\n", DateTime.Now.ToString(), text));
		}
	}
}
