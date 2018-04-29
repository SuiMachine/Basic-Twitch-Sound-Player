using System;
using System.Drawing;
using System.Windows.Forms;

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

    public class OverridenColorTable : ProfessionalColorTable
    {
        public Color ColorMenuBorder { get; set; }
        public Color ColorMenuItemSelected { get; set; }
        public Color ColorMenuBackground { get; set; }
        public Color TextColor { get; set; }

        public OverridenColorTable()
        {
            ColorMenuBorder = base.MenuBorder;
            ColorMenuItemSelected = base.MenuItemSelected;
            ColorMenuBackground = base.ToolStripDropDownBackground;
            TextColor = Color.Black;
        }

        public override Color MenuBorder => ColorMenuBorder;
        public override Color ButtonCheckedGradientBegin => base.ButtonCheckedGradientBegin;
        public override Color ButtonCheckedGradientEnd => base.ButtonCheckedGradientEnd;
        public override Color ButtonCheckedGradientMiddle => base.ButtonCheckedGradientMiddle;
        public override Color ButtonCheckedHighlight => base.ButtonCheckedHighlight;
        public override Color ButtonCheckedHighlightBorder => base.ButtonCheckedHighlightBorder;
        public override Color ButtonPressedBorder => base.ButtonPressedBorder;
        public override Color ButtonPressedGradientBegin => base.ButtonPressedGradientBegin;
        public override Color ButtonPressedGradientEnd => base.ButtonPressedGradientEnd;
        public override Color ButtonPressedGradientMiddle => base.ButtonPressedGradientMiddle;
        public override Color ButtonPressedHighlight => base.ButtonPressedHighlight;
        public override Color ButtonPressedHighlightBorder => base.ButtonPressedHighlightBorder;
        public override Color ButtonSelectedBorder => base.ButtonSelectedBorder;
        public override Color ButtonSelectedHighlight => base.ButtonSelectedHighlight;
        public override Color ButtonSelectedHighlightBorder => base.ButtonSelectedHighlightBorder;
        public override Color CheckBackground => ColorMenuBackground;
        public override Color CheckPressedBackground => base.CheckPressedBackground;
        public override Color CheckSelectedBackground => ColorMenuItemSelected;
        public override Color MenuItemBorder => ColorMenuBorder;
        public override Color MenuItemPressedGradientBegin => base.MenuItemPressedGradientBegin;
        public override Color MenuItemPressedGradientEnd => base.MenuItemPressedGradientEnd;
        public override Color MenuItemPressedGradientMiddle => base.MenuItemPressedGradientMiddle;
        public override Color MenuItemSelected => ColorMenuItemSelected;
        public override Color ToolStripBorder => ColorMenuBorder;
        public override Color MenuStripGradientBegin => Color.Black;
        public override Color MenuStripGradientEnd => Color.Yellow;
        public override Color ToolStripDropDownBackground => ColorMenuBackground;
    }
}
