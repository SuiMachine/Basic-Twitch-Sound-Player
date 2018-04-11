using System;
using System.Drawing;

namespace BasicTwitchSoundPlayer.Extensions
{
    class ColorExtension
    {
        public static Color ParseColor(string ColorAsText, Color colorRef)
        {
            if (ColorAsText != String.Empty)
            {
                if (int.TryParse(ColorAsText, out int color))
                {
                    return Color.FromArgb(color);
                }
            }
            return colorRef;
        }
    }
}
