using HenHen.Framework.Extensions;

namespace HenHen.Framework.Graphics
{
    public class Sprite : Drawable
    {
        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);
        private Raylib_cs.Texture2D texture;
        private bool isTextureLoaded;

        protected void LoadTexture(string path)
        {
            UnloadTexture(texture);
            texture = Raylib_cs.Raylib.LoadTexture(path);
            isTextureLoaded = true;
        }
        protected override void OnRender()
        {
            base.OnRender();
            Raylib_cs.Raylib.DrawTexture(texture, (int)GetRenderPosition().X, (int)GetRenderPosition().Y, Color.ToRaylibColor());
        }

        protected void UnloadTexture(Raylib_cs.Texture2D texture)
        {
            if(isTextureLoaded)
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
