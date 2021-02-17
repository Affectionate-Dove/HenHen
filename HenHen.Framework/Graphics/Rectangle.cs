using HenHen.Framework.Extensions;

namespace HenHen.Framework.Graphics
{
    public class Rectangle : Drawable, IHasColor
    {
        public ColorInfo Color { get; set; }

        protected override void OnRender()
        {
            base.OnRender();
            var renderPos = GetRenderPosition();
            var renderSize = GetRenderSize();
            Raylib_cs.Raylib.DrawRectangle((int)renderPos.X, (int)renderPos.Y, (int)renderSize.X, (int)renderSize.Y, Color.ToRaylibColor());
        }
    }
}
