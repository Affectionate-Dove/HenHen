﻿using HenHen.Framework.Extensions;

namespace HenHen.Framework.Graphics2d
{
    public class Rectangle : Drawable, IHasColor
    {
        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);

        protected override void OnRender()
        {
            base.OnRender();
            var rectPos = LayoutInfo.RenderRect;
            Raylib_cs.Raylib.DrawRectangle((int)rectPos.Left, (int)rectPos.Top, (int)rectPos.Width, (int)rectPos.Height, Color.ToRaylibColor());
        }
    }
}
