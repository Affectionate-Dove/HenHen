// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenFwork.Graphics2d
{
    public struct MarginPadding
    {
        public float Top { get; set; }
        public float Bottom { get; set; }
        public float Left { get; set; }
        public float Right { get; set; }

        public Vector2 TopLeft
        {
            get => new(Left, Top);
            set
            {
                Top = value.Y;
                Left = value.X;
            }
        }

        public Vector2 BottomRight
        {
            get => new(Right, Bottom);
            set
            {
                Bottom = value.Y;
                Right = value.X;
            }
        }

        public float TotalHorizontal
        {
            get => Left + Right;
            set => Left = Right = value * 0.5f;
        }

        public float TotalVertical
        {
            get => Top + Bottom;
            set => Top = Bottom = value * 0.5f;
        }

        public float Horizontal
        {
            set => Left = Right = value;
        }

        public float Vertical
        {
            set => Top = Bottom = value;
        }

        public Vector2 Total => new(TotalHorizontal, TotalVertical);
    }
}