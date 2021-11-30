// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Graphics;
using System;
using System.Numerics;

namespace HenFwork.Graphics2d
{
    public class Line : Drawable
    {
        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);
        public float Thickness { get; set; } = 1;
        public Vector2 A { get; set; }
        public Vector2 B { get; set; }

        protected override void OnRender()
        {
            base.OnRender();
            var rect = LayoutInfo.RenderRect;
            Drawing.DrawLine(A + rect.TopLeft, B + rect.TopLeft, Thickness, Color);
        }

        protected override Vector2 ComputeRenderSize() => new(Math.Abs(A.X - B.X), Math.Abs(A.Y - B.Y));
    }
}