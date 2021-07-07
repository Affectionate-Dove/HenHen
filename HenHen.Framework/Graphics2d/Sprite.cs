// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    /// <summary>
    ///     Draws a provided texture.
    /// </summary>
    public class Sprite : Drawable
    {
        public Raylib_cs.Texture2D Texture { get; set; }

        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);

        protected override void OnRender()
        {
            base.OnRender();
            var rect = LayoutInfo.RenderRect;
            var sourceRec = new Raylib_cs.Rectangle(0, 0, Texture.width, Texture.height);
            var destRec = new Raylib_cs.Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
            Raylib_cs.Raylib.DrawTexturePro(Texture, sourceRec, destRec, Vector2.Zero, 0, Color);
        }
    }
}