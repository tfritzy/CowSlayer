
using UnityEngine;

namespace Extensions
{
    public static class ColorExtensions
    {
        public static Color Create(int r, int g, int b)
        {
            return new Color(r / 255f, g / 255f, b / 255f);
        }
    }
}
