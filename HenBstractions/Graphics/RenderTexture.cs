// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenBstractions.Graphics
{
    public class RenderTexture : Texture
    {
        ~RenderTexture() => Raylib_cs.Raylib.UnloadRenderTexture(RenderTexture2D);

        internal Raylib_cs.RenderTexture2D RenderTexture2D { get; }

        public RenderTexture(Vector2 size) => RenderTexture2D = Raylib_cs.Raylib.LoadRenderTexture((int)size.X, (int)size.Y);
    }
}