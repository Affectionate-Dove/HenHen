using HenHen.Framework.Extensions;

namespace HenHen.Framework.Graphics
{
    public class Rectangle : Drawable, IHasColor
    {
        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);

        protected override void OnRender()
        {
            base.OnRender();
            var rectPos = GetRenderRect();
            Raylib_cs.Raylib.DrawRectangle((int)rectPos.X, (int)rectPos.Y, (int)rectPos.Width, (int)rectPos.Height, Color.ToRaylibColor());
        }
    }
}
