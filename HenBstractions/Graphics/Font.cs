// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenBstractions.Graphics
{
    public class Font
    {
        ~Font() => Raylib_cs.Raylib.UnloadFont(InternalFont);

        public static Font DefaultFont { get; } = new() { InternalFont = Raylib_cs.Raylib.GetFontDefault() };
        internal Raylib_cs.Font InternalFont { get; private init; }

        public Font(string path)
        {
            InternalFont = Raylib_cs.Raylib.LoadFont(path);

            // todo: another constructor with filters to choose?
            Raylib_cs.Raylib.SetTextureFilter(InternalFont.texture, Raylib_cs.TextureFilter.TEXTURE_FILTER_BILINEAR);
        }

        private Font()
        { }
    }
}