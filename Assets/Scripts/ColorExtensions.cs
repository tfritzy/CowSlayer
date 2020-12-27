
using System;
using UnityEngine;

namespace Extensions
{
    public static class ColorExtensions
    {
        public static Color Create(string colorHexCode)
        {
            int r = Convert.ToInt32("0x" + colorHexCode.Substring(0, 2), 16);
            int g = Convert.ToInt32("0x" + colorHexCode.Substring(2, 2), 16);
            int b = Convert.ToInt32("0x" + colorHexCode.Substring(4, 2), 16);
            return ColorExtensions.Create(r, g, b);
        }

        public static Color Create(int r, int g, int b)
        {
            return new Color(r / 255f, g / 255f, b / 255f);
        }

        public static Color Lighten(Color color)
        {
            float a = color.a;
            color *= 2;
            color.a = a;
            return color;
        }
    }
}
