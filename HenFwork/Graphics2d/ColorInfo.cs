// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenFwork.Graphics2d
{
    public struct ColorInfo
    {
        public byte a;
        public byte r;
        public byte b;
        public byte g;

        public ColorInfo(byte r, byte g, byte b, byte a = 255)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public static implicit operator Raylib_cs.Color(ColorInfo c) => new(c.r, c.g, c.b, c.a);

        public static implicit operator ColorInfo(Raylib_cs.Color c) => new(c.r, c.g, c.b, c.a);
    }
}