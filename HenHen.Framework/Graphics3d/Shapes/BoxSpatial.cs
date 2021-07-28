// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Numerics;

namespace HenHen.Framework.Graphics3d.Shapes
{
    public class BoxSpatial : Spatial, IHasColor
    {
        public Box Box { get; set; }

        public ColorInfo? Color { get; set; } = Raylib_cs.Color.RAYWHITE;
        public ColorInfo? WireColor { get; set; }

        ColorInfo IHasColor.Color => Color.GetValueOrDefault(new(0, 0, 0, 0));

        protected override void OnRender()
        {
            if (Color.HasValue)
                Raylib_cs.Raylib.DrawCubeV(Box.Center + Position, Box.Size, Color.Value);
            if (WireColor is not null)
                Raylib_cs.Raylib.DrawCubeWiresV(Box.Center + Position, Box.Size, WireColor.Value);
        }
    }
}