// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Graphics;

namespace HenFwork.Graphics2d
{
    public class Rectangle : Drawable, IHasColor
    {
        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);
        public ColorInfo BorderColor { get; set; }
        public int BorderThickness { get; set; } = 0;

        protected override void OnRender()
        {
            base.OnRender();
            var rectPos = LayoutInfo.RenderRect;

            // draw fill
            if (Color.a != 0)
                Drawing.DrawRectangle(rectPos, Color);

            // draw border
            if (BorderColor.a != 0 && BorderThickness > 0)
                Drawing.DrawRectangleLines(rectPos, BorderThickness, BorderColor);
        }
    }
}