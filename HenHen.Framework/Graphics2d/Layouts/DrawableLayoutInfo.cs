// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public readonly struct DrawableLayoutInfo
    {
        public Vector2 LocalPosition { get; init; }
        public Vector2 RenderPosition { get; init; }
        public Vector2 RenderSize { get; init; }

        public RectangleF? MaskArea { get; init; }

        public Vector2 Origin { get; init; }

        public RectangleF LocalRect => new() { TopLeft = LocalPosition, Size = RenderSize };

        public RectangleF RenderRect => ComputeRenderRect(RenderPosition, RenderSize, Origin);

        public static RectangleF ComputeRenderRect(Vector2 renderPosition, Vector2 renderSize, Vector2 origin)
        {
            var renderPos = renderPosition - (renderSize * origin);
            return new RectangleF { TopLeft = renderPos, Size = renderSize };
        }
    }
}