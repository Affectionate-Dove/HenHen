// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenFwork.Numerics
{
    /// <summary>
    ///     A rectangular cuboid in 3D space.
    /// </summary>
    public readonly struct Box
    {
        public float Bottom { get; }

        public float Top { get; }

        public float Left { get; }

        public float Right { get; }

        public float Front { get; }

        public float Back { get; }

        public Vector3 Center => new((Left + Right) * 0.5f, (Top + Bottom) * 0.5f, (Front + Back) * 0.5f);

        public Vector3 Size => new(Right - Left, Top - Bottom, Front - Back);

        public Box(float left, float right, float bottom, float top, float back, float front)
        {
            Left = left;
            Right = right;
            Bottom = bottom;
            Top = top;
            Back = back;
            Front = front;
        }

        public static Box operator +(Box box, Vector3 v) => new(box.Left + v.X, box.Right + v.X, box.Bottom + v.Y, box.Top + v.Y, box.Back + v.Z, box.Front + v.Z);

        public static Box operator *(Box box, Vector3 v) => new(box.Left * v.X, box.Right * v.X, box.Bottom * v.Y, box.Top * v.Y, box.Back * v.Z, box.Front * v.Z);

        public static Box operator -(Box box, Vector3 v) => new(box.Left - v.X, box.Right - v.X, box.Bottom - v.Y, box.Top - v.Y, box.Back - v.Z, box.Front - v.Z);

        public static Box operator /(Box box, Vector3 v) => new(box.Left / v.X, box.Right / v.X, box.Bottom / v.Y, box.Top / v.Y, box.Back / v.Z, box.Front / v.Z);

        public static Box FromPositionAndSize(Vector3 position, Vector3 size, Vector3 origin) => FromPositionAndSize(position, size) - (size * origin);

        public static Box FromPositionAndSize(Vector3 position, Vector3 size) => new(position.X, position.X + size.X, position.Y, position.Y + size.Y, position.Z, position.Z + size.Z);

        public RectangleF ToTopDownRectangle() => new(Left, Right, Back, Front);
    }
}