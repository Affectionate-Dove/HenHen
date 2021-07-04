// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public class Sprite : Drawable
    {
        private Raylib_cs.Texture2D texture;

        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);

        public void SetTexture(string path) => texture = Game.TextureStore.Get(path);

        protected override void OnRender()
        {
            base.OnRender();
            var rect = LayoutInfo.RenderRect;
            var sourceRec = new Raylib_cs.Rectangle(0, 0, texture.width, texture.height);
            var destRec = new Raylib_cs.Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
            Raylib_cs.Raylib.DrawTexturePro(texture, sourceRec, destRec, Vector2.Zero, 0, Color);
        }
    }
}