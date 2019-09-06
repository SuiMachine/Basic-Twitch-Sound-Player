using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BasicTwitchSoundPlayer
{
    public class ColorWrapper
    {
        [XmlIgnore]
        public Color color;

        public ColorWrapper()
        {
            color = Color.Black;
        }

        public static implicit operator ColorWrapper(Color c)
        {
            return new ColorWrapper() { color = c };
        }

        public static implicit operator Color(ColorWrapper cw)
        {
            return cw.color;
        }

        public override string ToString()
        {
            return color.ToArgb().ToString();
        }

        [XmlElement]
        public int ColorXML
        {
            get { return color.ToArgb(); }
            set { color = Color.FromArgb(value); }
        }
    }
}
