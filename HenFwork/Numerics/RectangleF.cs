// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;
using System.Numerics;

namespace HenFwork.Numerics
{
    /// <summary>
    ///     Represents a rectangle shape.
    /// </summary>
    /// <remarks>
    ///     If <see cref="Top"/> is greater than or equal to <see cref="Bottom"/>,
    ///     the coordinate space is considered to be y-up, otherwise y-down.
    /// </remarks>
    public readonly struct RectangleF
    {
        public float Left { get; }
        public float Right { get; }
        public float Top { get; }
        public float Bottom { get; }

        public float Area => Math.Abs(Width * Height);
        public Vector2 Center => new((Left + Right) * 0.5f, (Top + Bottom) * 0.5f);

        /// <value>
        ///     <see cref="CoordinateSystem2d.YUp"/> if <see cref="Top"/>
        ///     >= <see cref="Bottom"/>, otherwise <see cref="CoordinateSystem2d.YDown"/>.
        /// </value>
        public CoordinateSystem2d CoordinateSystem => Top >= Bottom ? CoordinateSystem2d.YUp : CoordinateSystem2d.YDown;

        /// <summary>
        /// If <see cref="Left"/> is greater than <see cref="Right"/>,
        /// this will be negative.
        /// Setter: sets <see cref="Right"/> to be
        /// equal to <see cref="Left"/> + <see cref="Width"/>.
        /// </summary>
        public float Width => Right - Left;

        /// <summary>
        /// If <see cref="Top"/> is greater than <see cref="Bottom"/>,
        /// this will be negative.
        /// Setter: sets <see cref="Bottom"/> to be
        /// equal to <see cref="Top"/> + <see cref="Height"/>.
        /// </summary>
        public float Height => Math.Abs(Bottom - Top);

        /// <summary>
        /// Shorthand for <see cref="Width"/>
        /// and <see cref="Height"/>.
        /// </summary>
        public Vector2 Size => new(Width, Height);

        public Vector2 TopLeft => new(Left, Top);
        public Vector2 TopRight => new(Right, Top);
        public Vector2 BottomLeft => new(Left, Bottom);
        public Vector2 BottomRight => new(Right, Bottom);

        public RectangleF(float left, float right, float bottom, float top)
        {
            Left = left;
            Right = right;
            Bottom = bottom;
            Top = top;
        }

        public static RectangleF operator +(RectangleF rectangle, Vector2 v) => new(rectangle.Left + v.X, rectangle.Right + v.X, rectangle.Bottom + v.Y, rectangle.Top + v.Y);

        public static RectangleF operator -(RectangleF rectangle, Vector2 v) => new(rectangle.Left - v.X, rectangle.Right - v.X, rectangle.Bottom - v.Y, rectangle.Top - v.Y);

        public static RectangleF operator *(RectangleF rectangle, Vector2 v) => new(rectangle.Left * v.X, rectangle.Right * v.X, rectangle.Bottom * v.Y, rectangle.Top * v.Y);

        public static RectangleF operator /(RectangleF rectangle, Vector2 v) => new(rectangle.Left / v.X, rectangle.Right / v.X, rectangle.Bottom / v.Y, rectangle.Top / v.Y);

        public static RectangleF FromPositionAndSize(Vector2 position, Vector2 size, Vector2 origin, CoordinateSystem2d coordinateSystem) => FromPositionAndSize(position, size, coordinateSystem) - (size * origin);

        public static RectangleF FromPositionAndSize(Vector2 position, Vector2 size, CoordinateSystem2d coordinateSystem)
        {
            var rect = new RectangleF(position.X, position.X + size.X, position.Y, position.Y + size.Y);
            if (coordinateSystem == CoordinateSystem2d.YDown)
                rect = new(rect.Left, rect.Right, rect.Top, rect.Bottom);
            return rect;
        }

        public override string ToString() => $"{{{nameof(Left)}={Left},{nameof(Top)}={Top},{nameof(Right)}={Right},{nameof(Bottom)}={Bottom}";

        public RectangleF? GetIntersection(RectangleF? other) => other.HasValue ? GetIntersectionNonNullableParameter(other.Value) : null;

        private RectangleF? GetIntersectionNonNullableParameter(RectangleF other)
        {
            if (CoordinateSystem != other.CoordinateSystem)
                throw new ArgumentException($"Coordinate system doesn't match. {nameof(other)}'s {nameof(CoordinateSystem)} is {other.CoordinateSystem}, while this {nameof(RectangleF)}'s is {CoordinateSystem}.", nameof(other));

            if (Left > other.Right || Right < other.Left || (CoordinateSystem == CoordinateSystem2d.YUp && (Bottom > other.Top || Top < other.Bottom)) || (CoordinateSystem == CoordinateSystem2d.YDown && (Bottom < other.Top || Top > other.Bottom)))
                return null;

            if (CoordinateSystem == CoordinateSystem2d.YUp)
                return new(Math.Max(Left, other.Left), Math.Min(Right, other.Right), Math.Max(Bottom, other.Bottom), Math.Min(Top, other.Top));
            else
                return new(Math.Max(Left, other.Left), Math.Min(Right, other.Right), Math.Min(Bottom, other.Bottom), Math.Max(Top, other.Top));
        }
    }
}