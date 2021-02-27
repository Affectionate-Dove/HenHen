using HenHen.Framework.Extensions;
using HenHen.Framework.Graphics2d;

namespace HenHen.Framework.UI
{
    public class SpriteText : Drawable, IHasColor
    {
        public string Text { get; set; }

        public Raylib_cs.Font Font { get; set; } = Raylib_cs.Raylib.GetFontDefault();

        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);

        public float FontSize { get; set; } = 16;

        public float Spacing { get; set; } = 1;

        public bool AlignMiddle { get; set; }

        protected override void OnRender()
        {
            base.OnRender();
            var r = LayoutInfo.RenderRect;
            var size = Raylib_cs.Raylib.MeasureTextEx(Font, Text, FontSize, Spacing);
            var containingSize = LayoutInfo.RenderSize;

            if (AlignMiddle && size.X <= containingSize.X)
            {
                var halfDiff = (containingSize - size) * 0.5f;
                r.TopLeft += halfDiff;
            }

            Raylib_cs.Raylib.DrawTextRec(Font, Text, new Raylib_cs.Rectangle(r.Left, r.Top, r.Width, r.Height), FontSize, Spacing, size.X > Size.X, Color.ToRaylibColor());
        }
    }
}
