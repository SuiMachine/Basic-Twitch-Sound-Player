﻿namespace SSC
{
	public static class Mathf
	{
		public static int Clamp(int value, int min, int max)
		{
			if (value < min)
				return min;
			else if (value > max)
				return max;
			else
				return value;
		}
	}
}
