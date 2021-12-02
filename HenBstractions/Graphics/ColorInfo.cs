// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenBstractions.Graphics
{
    public struct ColorInfo
    {
        public byte a;
        public byte r;
        public byte b;
        public byte g;

        public static ColorInfo LIGHTGRAY { get; } = Raylib_cs.Color.LIGHTGRAY;
        public static ColorInfo GRAY { get; } = Raylib_cs.Color.GRAY;
        public static ColorInfo DARKGRAY { get; } = Raylib_cs.Color.DARKGRAY;
        public static ColorInfo YELLOW { get; } = Raylib_cs.Color.YELLOW;
        public static ColorInfo GOLD { get; } = Raylib_cs.Color.GOLD;
        public static ColorInfo ORANGE { get; } = Raylib_cs.Color.ORANGE;
        public static ColorInfo PINK { get; } = Raylib_cs.Color.PINK;
        public static ColorInfo RED { get; } = Raylib_cs.Color.RED;
        public static ColorInfo MAROON { get; } = Raylib_cs.Color.MAROON;
        public static ColorInfo GREEN { get; } = Raylib_cs.Color.GREEN;
        public static ColorInfo LIME { get; } = Raylib_cs.Color.LIME;
        public static ColorInfo DARKGREEN { get; } = Raylib_cs.Color.DARKGREEN;
        public static ColorInfo SKYBLUE { get; } = Raylib_cs.Color.SKYBLUE;
        public static ColorInfo BLUE { get; } = Raylib_cs.Color.BLUE;
        public static ColorInfo DARKBLUE { get; } = Raylib_cs.Color.DARKBLUE;
        public static ColorInfo PURPLE { get; } = Raylib_cs.Color.PURPLE;
        public static ColorInfo VIOLET { get; } = Raylib_cs.Color.VIOLET;
        public static ColorInfo DARKPURPLE { get; } = Raylib_cs.Color.DARKPURPLE;
        public static ColorInfo BEIGE { get; } = Raylib_cs.Color.BEIGE;
        public static ColorInfo BROWN { get; } = Raylib_cs.Color.BROWN;
        public static ColorInfo DARKBROWN { get; } = Raylib_cs.Color.DARKBROWN;
        public static ColorInfo WHITE { get; } = Raylib_cs.Color.WHITE;
        public static ColorInfo BLACK { get; } = Raylib_cs.Color.BLACK;
        public static ColorInfo BLANK { get; } = Raylib_cs.Color.BLANK;
        public static ColorInfo MAGENTA { get; } = Raylib_cs.Color.MAGENTA;
        public static ColorInfo RAYWHITE { get; } = Raylib_cs.Color.RAYWHITE;

        public ColorInfo(byte r, byte g, byte b, byte a = 255)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public static implicit operator Raylib_cs.Color(ColorInfo c) => new(c.r, c.g, c.b, c.a);

        public static implicit operator ColorInfo(Raylib_cs.Color c) => new(c.r, c.g, c.b, c.a);

        public ColorInfo WithAlpha(float alpha) => Raylib_cs.Raylib.ColorAlpha(this, alpha);
    }
}