using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public class RectangleF
    {
        public float Left { get; set; }
        public float Right { get; set; }
        public float Top { get; set; }
        public float Bottom { get; set; }

        public float Width => Right - Left;
        public float Height => Bottom - Top;
        public float Area => Width * Height;
        public Vector2 Size => new Vector2(Width, Height);
        public Vector2 Center => TopLeft + (Size / 2);

        public Vector2 TopLeft
        {
            get => new Vector2(Left, Top);
            set
            {
                Top = value.Y;
                Left = value.X;
            }
        }

        public Vector2 BottomRight
        {
            get => new Vector2(Right, Bottom);
            set
            {
                Bottom = value.Y;
                Right = value.X;
            }
        }
    }
}
