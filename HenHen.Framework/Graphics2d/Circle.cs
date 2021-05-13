// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Extensions;
using System;

namespace HenHen.Framework.Graphics2d
{
    public class Circle : Drawable
    {
        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);

        protected override void OnRender()
        {
            base.OnRender();
            var rect = LayoutInfo.RenderRect;
            var radius = MathF.Min(rect.Width, rect.Height) * 0.5f;
            Raylib_cs.Raylib.DrawCircle((int)rect.Center.X, (int)rect.Center.Y, radius, Color.ToRaylibColor());
        }
    }
}