// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using System.Numerics;

namespace HenHen.Framework.Collisions
{
    /// <summary>
    /// Provides elementary collision checks.
    /// </summary>
    public static class ElementaryCollisions
    {
        /// <remarks>A point on the edge of the circle is considered to be inside.</remarks>
        public static bool IsPointInCircle(Vector2 p, Circle c) => (c.CenterPosition - p).Length() <= c.Radius;

        /// <remarks>A point on the edge of a triangle is considered to be in it.</remarks>
        public static bool IsPointInTriangle(Vector2 p, Triangle2 t)
        {
            // credit: https://stackoverflow.com/a/20861130/6394285
            var s = (t.A.Y * t.C.X) - (t.A.X * t.C.Y) + ((t.C.Y - t.A.Y) * p.X) + ((t.A.X - t.C.X) * p.Y);
            var h = (t.A.X * t.B.Y) - (t.A.Y * t.B.X) + ((t.A.Y - t.B.Y) * p.X) + ((t.B.X - t.A.X) * p.Y);

            if ((s < 0) != (h < 0))
                return false;

            var A = (-t.B.Y * t.C.X) + (t.A.Y * (t.C.X - t.B.X)) + (t.A.X * (t.B.Y - t.C.Y)) + (t.B.X * t.C.Y);

            return A < 0 ?
                    (s <= 0 && s + h >= A) :
                    (s >= 0 && s + h <= A);
        }

        /// <remarks>A point on the edge of a rectangle is considered to be in it.</remarks>
        public static bool IsPointInRectangle(Vector2 p, RectangleF r)
        {
            if (r.CoordinateSystem == CoordinateSystem2d.YUp)
                return r.Left <= p.X && p.X <= r.Right && r.Top >= p.Y && p.Y >= r.Bottom;
            else
                return r.Left <= p.X && p.X <= r.Right && r.Top <= p.Y && p.Y <= r.Bottom;
        }

        /// <remarks>Touching edges are considered to be a collision.</remarks>
        public static bool AreCirclesColliding(Circle a, Circle b) => (a.CenterPosition - b.CenterPosition).Length() <= a.Radius + b.Radius;

        /// <remarks>Touching edges are considered to be a collision.</remarks>
        public static bool AreSpheresColliding(Sphere a, Sphere b) => (a.CenterPosition - b.CenterPosition).Length() <= a.Radius + b.Radius;

        /// <remarks>Touching edges are considered to be a collision.</remarks>
        public static bool AreRectanglesColliding(RectangleF a, RectangleF b)
        {
            var aIsToTheRightOfB = a.Left > b.Right;
            var aIsToTheLeftOfB = a.Right < b.Left;
            var aIsAboveB = a.Bottom > b.Top;
            var AIsBelowB = a.Top < b.Bottom;
            return !(aIsToTheRightOfB
              || aIsToTheLeftOfB
              || aIsAboveB
              || AIsBelowB);
        }

        public static bool AreBoxesColliding(Box a, Box b)
        {
            var aIsToTheRightOfB = a.Left > b.Right;
            var aIsToTheLeftOfB = a.Right < b.Left;
            var aIsAboveB = a.Bottom > b.Top;
            var aIsBelowB = a.Top < b.Bottom;
            var aIsInFrontOfB = a.Back > b.Front;
            var aIsBehindB = a.Front < b.Back;
            return !(aIsToTheRightOfB
              || aIsToTheLeftOfB
              || aIsAboveB
              || aIsBelowB
              || aIsInFrontOfB
              || aIsBehindB);
        }
    }
}