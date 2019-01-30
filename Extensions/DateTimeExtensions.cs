using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTwitchSoundPlayer.Extensions
{
    static class DateTimeExtensions
    {
        public static DateTime ToDateTimeSafe(this string Text)
        {
            if (Text == null || Text == String.Empty)
            {
                return DateTime.UtcNow;
            }
            else
            {
                if (DateTime.TryParse(Text, out DateTime result))
                {
                    return result;
                }
                else
                {
                    return DateTime.MinValue;
                }

            }
        }
    }
}
