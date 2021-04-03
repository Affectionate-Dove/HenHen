// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenHen.Framework.Numerics
{
    public struct Box
    {
        public float Bottom { get; set; }

        public float Top { get; set; }

        public float Left { get; set; }

        public float Right { get; set; }

        public float Front { get; set; }

        public float Back { get; set; }

        public Box(float left, float right, float bottom, float top, float back, float front)
        {
            Left = left;
            Right = right;
            Bottom = bottom;
            Top = top;
            Back = back;
            Front = front;
        }

        public RectangleF ToTopDownRectangle() => new()
        {
            Left = Left,
            Right = Right,
            Top = Front,
            Bottom = Back
        };
    }
}