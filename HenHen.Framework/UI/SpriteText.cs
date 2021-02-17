using HenHen.Framework.Extensions;
using HenHen.Framework.Graphics;

namespace HenHen.Framework.UI
{
    public class SpriteText : Drawable, IHasColor
    {
        public string Text { get; set; }

        public Raylib_cs.Font Font { get; set; } = Raylib_cs.Raylib.GetFontDefault();

        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);

        protected override void OnRender()
        {
            base.OnRender();
            var r = GetRenderRect();
            Raylib_cs.Raylib.DrawTextRec(Font, Text, new Raylib_cs.Rectangle(r.Left, r.Top, r.Width, r.Height), 12, 1, true, Color.ToRaylibColor());
        }
    }
}
