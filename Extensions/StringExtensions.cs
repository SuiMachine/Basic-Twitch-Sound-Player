using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTwitchSoundPlayer.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveWhitespaces(this string Text)
        {
            return new string(Text.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}
