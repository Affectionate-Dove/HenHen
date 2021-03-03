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

        public RectangleF LocalRect => new RectangleF { TopLeft = LocalPosition, Size = RenderSize };

        public RectangleF RenderRect
        {
            get
            {
                var renderPos = RenderPosition - (RenderSize * Origin);
                return new RectangleF { TopLeft = renderPos, Size = RenderSize };
            }
        }
    }
}
