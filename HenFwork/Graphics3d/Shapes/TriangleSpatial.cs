// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Graphics;
using HenBstractions.Numerics;
using HenFwork.Graphics2d;

namespace HenFwork.Graphics3d.Shapes
{
    public class TriangleSpatial : Spatial, IHasColor
    {
        public Triangle3 Triangle { get; set; }

        public ColorInfo? Color { get; set; } = ColorInfo.RAYWHITE;
        public ColorInfo? WireColor { get; set; }

        ColorInfo IHasColor.Color => Color ?? ColorInfo.BLANK;

        protected override void OnRender()
        {
            if (Color.HasValue)
                Drawing.DrawTriangle3D(Triangle, Color.Value);

            if (WireColor.HasValue)
            {
                var normal = Triangle.Normal;

                // in order for all lines to be more visible,
                // move them above the triangle slightly
                var linesTriangle = Triangle + (normal * 0.00005f);

                Drawing.DrawLine3D(linesTriangle.A, linesTriangle.B, WireColor.Value);
                Drawing.DrawLine3D(linesTriangle.B, linesTriangle.C, WireColor.Value);
                Drawing.DrawLine3D(linesTriangle.A, linesTriangle.C, WireColor.Value);
            }
        }
    }
}