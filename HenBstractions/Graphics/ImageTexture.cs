// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenBstractions.Graphics
{
    public class ImageTexture : Texture
    {
        ~ImageTexture() => Raylib_cs.Raylib.UnloadTexture(Texture2D);

        public ImageTexture(string path) => Texture2D = Raylib_cs.Raylib.LoadTexture(path);
    }
}