using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Faktformer_0._0._1
{
    internal class ColorHexRgbConverter
    {
        public static Color HexToColor(string hexColor)
        {
            //Remove # if present
            if (hexColor.IndexOf('#') != -1)
                hexColor = hexColor.Replace("#", "");

            byte red = 0;
            byte green = 0;
            byte blue = 0;

            if (hexColor.Length == 6)
            {
                //#RRGGBB
                red = byte.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                green = byte.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                blue = byte.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }
            else if (hexColor.Length == 3)
            {
                //#RGB
                red = byte.Parse(hexColor[0].ToString() + hexColor[0].ToString(), NumberStyles.AllowHexSpecifier);
                green = byte.Parse(hexColor[1].ToString() + hexColor[1].ToString(), NumberStyles.AllowHexSpecifier);
                blue = byte.Parse(hexColor[2].ToString() + hexColor[2].ToString(), NumberStyles.AllowHexSpecifier);
            }
            return Color.FromRgb(red, green, blue);
        }
    }
}
