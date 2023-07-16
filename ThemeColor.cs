using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsMultiColor
{
    public static class ThemeColor
    {
        public static Color primaryColor { get; set; }
        public static Color secondaryColor { get; set; }
        public static List<string> ColorList = new List<string>()
        {
            "#ffab12",
            "#3ba9e6",
            "#7fff00",
            "#564080",
            "#ff9973",
            "#8effe2",
            "#fff89c",
            "#dcffa1",
        };

        public static Color ChangeColorBrightNess(Color color, double correctionFactor)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;

            // If correction factor less than 0, darken color
            if(correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            // If correction factor greater than 0, lighten color
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;

            }
            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }
    }
}
