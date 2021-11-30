// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenFwork.Graphics2d;
using HenFwork.Numerics;

namespace HenFwork.Graphics3d.Shapes
{
    public class TriangleSpatial : Spatial, IHasColor
    {
        public Triangle3 Triangle { get; set; }

        public ColorInfo? Color { get; set; } = Raylib_cs.Color.RAYWHITE;
        public ColorInfo? WireColor { get; set; }

        ColorInfo IHasColor.Color => Color ?? Raylib_cs.Color.BLANK;

        protected override void OnRender()
        {
            if (Color.HasValue)
                Raylib_cs.Raylib.DrawTriangle3D(Triangle.A, Triangle.B, Triangle.C, Color.Value);

            if (WireColor.HasValue)
            {
                var normal = Triangle.Normal;

                // in order for all lines to be more visible,
                // move them above the triangle slightly
                var linesTriangle = Triangle + (normal * 0.00005f);

                Raylib_cs.Raylib.DrawLine3D(linesTriangle.A, linesTriangle.B, WireColor.Value);
                Raylib_cs.Raylib.DrawLine3D(linesTriangle.B, linesTriangle.C, WireColor.Value);
                Raylib_cs.Raylib.DrawLine3D(linesTriangle.A, linesTriangle.C, WireColor.Value);
            }
        }
    }
}