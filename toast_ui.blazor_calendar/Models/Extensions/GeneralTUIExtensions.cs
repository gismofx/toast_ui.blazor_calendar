using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models.Extensions
{
    public static class GeneralTUIExtensions
    {
        /// <summary>
        /// Converts a Color to a Hex string for database use or other.
        /// </summary>
        /// <param name="color">The Color object.</param>
        /// <returns>Hexvalue</returns>
        public static string ToHex(this Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        /// <summary>
        /// Converts a Hex string to a Color.
        /// </summary>
        /// <param name="hexValue">The Hexvalue</param>
        /// <returns>Color</returns>
        public static Color ToColor(this string hexValue)
        {
            if (hexValue != null && hexValue.StartsWith("#"))
                return ColorTranslator.FromHtml(hexValue);

            return Color.Black; // Default color
        }
    }
}
