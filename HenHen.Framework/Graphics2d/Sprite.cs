using HenHen.Framework.Extensions;
using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public class Sprite : Drawable
    {
        private Raylib_cs.Texture2D texture;

        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);
        /// <summary>
        /// Loads a texture from a file path. Previous texture is automatically unloaded.
        /// </summary>
        /// <param name="path"></param>

        protected override void OnRender()
        {
            base.OnRender();
            var rect = LayoutInfo.RenderRect;
            var sourceRec = new Raylib_cs.Rectangle(0, 0, texture.width, texture.height);
            var destRec = new Raylib_cs.Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
            Raylib_cs.Raylib.DrawTexturePro(texture, sourceRec, destRec, Vector2.Zero, 0, Color.ToRaylibColor());
        }
    }
}