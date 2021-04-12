// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Extensions;
using System.Numerics;

namespace HenHen.Framework.Graphics2d
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
            Raylib_cs.Raylib.DrawLineEx(A + rect.TopLeft, B + rect.TopLeft, Thickness, Color.ToRaylibColor());
        }
    }
}