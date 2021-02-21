using HenHen.Framework.Extensions;

namespace HenHen.Framework.Graphics
{
    class Sprite : Drawable
    {
        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);
        private Raylib_cs.Texture2D texture;

        protected void LoadTexture(string path)
        {
            texture = Raylib_cs.Raylib.LoadTexture(path);
        }
        protected override void OnRender()
        {
            base.OnRender();
            Raylib_cs.Raylib.DrawTexture(texture, (int)GetRenderPosition().X, (int)GetRenderPosition().Y, Color.ToRaylibColor());
        }

        protected void UnloadTexture(Raylib_cs.Texture2D texture)
        {
            Raylib_cs.Raylib.UnloadTexture(texture);
        }
        ~Sprite()
        {
            UnloadTexture(texture);
        }
    }
}
