using System;
using System.Linq;

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
