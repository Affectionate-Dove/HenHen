// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public struct DrawableLayoutInfo
    {
        public Vector2 LocalPosition { get; init; }
        public Vector2 RenderPosition { get; init; }
        public Vector2 RenderSize { get; init; }

        public Vector2 Origin { get; init; }

        public RectangleF LocalRect => RectangleF.FromPositionAndSize(LocalPosition, RenderSize, Origin, CoordinateSystem2d.YDown);

        public RectangleF RenderRect => RectangleF.FromPositionAndSize(RenderPosition, RenderSize, Origin, CoordinateSystem2d.YDown);
    }
}