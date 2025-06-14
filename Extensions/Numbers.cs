namespace SSC.Extensions
{
	static class NumberTypesExtension
	{
		public static float ToFloat(this string Text, float DEFAULT_VALUE)
		{
			if (float.TryParse(Text, out float res))
				return res;
			else
				return DEFAULT_VALUE;
		}

		public static int ToInt(this string Text, int DEFAULT_VALUE)
		{
			if (int.TryParse(Text, out int res))
				return res;
			else
				return DEFAULT_VALUE;
		}

		public static bool ToBoolean(this string Text, bool DEFAULT_VALUE)
		{
			if (bool.TryParse(Text, out bool val))
			{
				return val;
			}
			else
				return DEFAULT_VALUE;
		}
	}
}
