using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
