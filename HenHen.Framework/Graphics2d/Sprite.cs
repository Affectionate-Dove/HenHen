using HenHen.Framework.Extensions;
using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public class Sprite : Drawable
    {
        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);
        private Raylib_cs.Texture2D texture;
        private bool isTextureLoaded;

        /// <summary>
        /// Loads a texture from a file path. Previous texture is automatically unloaded.
        /// </summary>
        /// <param name="path"></param>
        public void SetTexture(string path)
        {
            UnloadTexture(texture);
            texture = Raylib_cs.Raylib.LoadTexture(path);
            isTextureLoaded = true;
        }
        protected override void OnRender()
        {
            if (!isTextureLoaded)
            {
                return;
            }
            base.OnRender();
            var rect = GetRenderRect();
            var sourceRec = new Raylib_cs.Rectangle(0, 0, texture.width, texture.height);
            var destRec = new Raylib_cs.Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
            Raylib_cs.Raylib.DrawTexturePro(texture, sourceRec, destRec, Vector2.Zero, 0, Color.ToRaylibColor());
        }

        protected void UnloadTexture(Raylib_cs.Texture2D texture)
        {
            if (isTextureLoaded)
            {
                Raylib_cs.Raylib.UnloadTexture(texture);
            }
        }
        ~Sprite()
        {
            UnloadTexture(texture);
        }
    }
}
