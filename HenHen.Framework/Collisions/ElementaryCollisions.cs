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

        public static bool IsPointInTriangle(Vector2 p, Triangle2 t) => Raylib_cs.Raylib.CheckCollisionPointTriangle(p, t.A, t.B, t.C);

        public static bool IsPointInRectangle(Vector2 p, RectangleF r) => Raylib_cs.Raylib.CheckCollisionPointRec(p, new Raylib_cs.Rectangle(r.Left, r.Top, r.Width, r.Height));

        /// <remarks>Touching edges aren't considered to be a collision.</remarks>
        public static bool AreCirclesColliding(Circle a, Circle b) => (a.CenterPosition - b.CenterPosition).Length() < a.Radius + b.Radius;

        /// <remarks>Touching edges aren't considered to be a collision.</remarks>
        public static bool AreSpheresColliding(Sphere a, Sphere b) => (a.CenterPosition - b.CenterPosition).Length() < a.Radius + b.Radius;
    }
}