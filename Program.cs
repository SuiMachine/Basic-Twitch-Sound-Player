using System;
using System.Threading;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer
{
	static class Program
	{
		static Mutex mutex;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			bool createdNew;
			mutex = new Mutex(true, "BasicTwitchSoundPlayerMutex", out createdNew);
			if (createdNew)
				Application.Run(new MainForm());
			else
				Application.Exit();
		}
	}
}
