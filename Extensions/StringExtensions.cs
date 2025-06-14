using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SSC.Extensions
{
	public static class StringExtensions
	{
		public static string RemoveWhitespaces(this string Text)
		{
			return new string(Text.ToCharArray()
				.Where(c => !Char.IsWhiteSpace(c))
				.ToArray());
		}

		public static string SanitizeTags(this string tag)
		{
			tag = tag.Replace('_', ' ');
			tag = Regex.Replace(tag, "[^a-zA-Z0-9|\\s]", "");
			while (tag.Contains("  "))
				tag = tag.Replace("  ", " ");
			return tag.Trim();
		}
	}
}
