// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Review License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Extensions;
using HenHen.Framework.Graphics2d;
using HenHen.Framework.Numerics;

namespace HenHen.Framework.Graphics3d
{
    public class TriangleSpatial : Spatial, IHasColor
    {
        public Triangle3 Triangle { get; set; }

        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);

        protected override void OnRender() => Raylib_cs.Raylib.DrawTriangle3D(Triangle.A, Triangle.B, Triangle.C, Color.ToRaylibColor());
    }
}