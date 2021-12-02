// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Graphics;
using HenBstractions.Numerics;
using HenFwork.Graphics2d;

namespace HenFwork.Graphics3d.Shapes
{
    public class BoxSpatial : Spatial, IHasColor
    {
        public Box Box { get; set; }

        public ColorInfo? Color { get; set; } = ColorInfo.RAYWHITE;
        public ColorInfo? WireColor { get; set; }

        ColorInfo IHasColor.Color => Color.GetValueOrDefault(new(0, 0, 0, 0));

        protected override void OnRender()
        {
            if (Color.HasValue)
                Drawing.DrawCube(Box.Center + Position, Box.Size, Color.Value);
            if (WireColor is not null)
                Drawing.DrawCubeWires(Box.Center + Position, Box.Size, WireColor.Value);
        }
    }
}