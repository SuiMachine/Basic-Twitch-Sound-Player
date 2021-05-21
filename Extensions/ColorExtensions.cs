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
		public Color ColorMenuBarBackground { get; set; }
		public Color ColorMenuBarSelected { get; set; }
		public Color ColorMenuBackground { get; set; }
		public Color ColorMenuItemSelected { get; set; }
		public Color TextColor { get; set; }

		public OverridenColorTable()
		{
			ColorMenuBorder = base.MenuBorder;
			ColorMenuBarBackground = base.MenuStripGradientBegin;
			ColorMenuItemSelected = base.MenuItemSelected;
			ColorMenuBackground = base.ToolStripDropDownBackground;
			TextColor = base.MenuBorder;
		}

		public override Color MenuBorder => ColorMenuBorder;

		public override Color CheckSelectedBackground => Transparent(ColorMenuItemSelected, 40);
		public override Color CheckBackground => Transparent(ColorMenuItemSelected, 40);
		public override Color CheckPressedBackground => Transparent(ColorMenuItemSelected, 40);

		private Color Transparent(Color originalColor, int Opacity)
		{
			int opacity = 255 * Opacity / 100;
			return Color.FromArgb(opacity, originalColor.R, originalColor.G, originalColor.B);
		}

		#region Menus
		public override Color MenuItemBorder => ColorMenuBorder;

		//MenuStrip Background
		public override Color MenuStripGradientBegin => ColorMenuBarBackground;
		public override Color MenuStripGradientEnd => ColorMenuBarBackground;

		//Menu Item Pressed
		public override Color MenuItemPressedGradientBegin => ColorMenuBarSelected;
		public override Color MenuItemPressedGradientMiddle => ColorMenuBarSelected;
		public override Color MenuItemPressedGradientEnd => ColorMenuBarSelected;

		//Menu Item Selected
		public override Color MenuItemSelected => ColorMenuItemSelected;
		public override Color MenuItemSelectedGradientBegin => ColorMenuItemSelected;
		public override Color MenuItemSelectedGradientEnd => ColorMenuItemSelected;
		#endregion

		//Toolstrip
		public override Color ToolStripBorder => ColorMenuBorder;

		public override Color ToolStripDropDownBackground => ColorMenuBackground;
	}
}
