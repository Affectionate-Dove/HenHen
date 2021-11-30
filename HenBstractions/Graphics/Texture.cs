// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenBstractions.Graphics
{
    public abstract class Texture
    {
        private Raylib_cs.Texture2D texture2D;
        public Vector2 Size => new(Texture2D.width, Texture2D.height);

        internal Raylib_cs.Texture2D Texture2D { get => texture2D; private protected set => texture2D = value; }

        public void GenerateMipmaps() => Raylib_cs.Raylib.GenTextureMipmaps(ref texture2D);
    }
}