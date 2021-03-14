// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Extensions;
using HenHen.Framework.Numerics;
using HenHen.Framework.Worlds.Mediums;
using HenHen.Framework.Worlds.Nodes;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Collisions
{
    public static class CollisionChecker
    {
        public static void CheckNodeCollisions(IReadOnlyList<Node> nodes, ICollisionHandler handler)
        {
            for (var i = 0; i < nodes.Count - 1; i++)
            {
                var a = nodes[i];
                if (a.CollisionBody is null)
                    continue;

                for (var j = i + 1; j < nodes.Count; j++)
                {
                    var b = nodes[j];
                    if (b.CollisionBody is null)
                        continue;

                    if (ElementaryCollisions.AreSpheresColliding(a.CollisionBody.ContainingSphere, b.CollisionBody.ContainingSphere))
                    {
                        if (a.CollisionBody.Spheres.Count is 1 && b.CollisionBody.Spheres.Count is 1)
                            handler.OnCollision(a, b);
                        else if (ThouroughlyCheckSpheresForCollision(a.CollisionBody.Spheres, b.CollisionBody.Spheres))
                            handler.OnCollision(a, b);
                    }
                }
            }
        }

        public static bool IsNodeContainedInMediums(Node node, IEnumerable<Medium> mediums)
        {
            if (node.CollisionBody is null)
                return IsPointContainedInMediums(node.Position.ToTopDownPoint(), mediums);

            foreach (var sphere in node.CollisionBody.Spheres)
            {
                if (!IsCircleContainedInMediums(sphere.ToTopDownCircle(), mediums))
                    return false;
            }
            return true;
        }

        private static IEnumerable<Vector2> SunflowerPoints(Circle circle, int pointsAmount)
        {
            // credit: https://stackoverflow.com/a/28572551

            const float alpha = 1;
            var boundaryPointsCount = (int)(alpha * MathF.Sqrt(pointsAmount));
            var phi = (MathF.Sqrt(5) + 1) / 2; // golden ratio

            float Radius(int k) => k > pointsAmount - boundaryPointsCount ? 1 : MathF.Sqrt(k - 0.5f) / MathF.Sqrt(pointsAmount - (boundaryPointsCount + 1) * 0.5f);

            for (var k = 1; k <= pointsAmount; k++)
            {
                var r = Radius(k) * circle.Radius;
                var theta = 2 * MathF.PI * k / (phi * phi);
                yield return new Vector2(r * MathF.Cos(theta), r * MathF.Sin(theta)) + circle.CenterPosition;
            }
        }

        private static bool IsPointContainedInMediums(Vector2 p, IEnumerable<Medium> mediums)
        {
            foreach (var medium in mediums)
            {
                if (ElementaryCollisions.IsPointInTriangle(p, medium.Triangle.ToTopDownTriangle()))
                    return true;
            }
            return false;
        }

        private static bool IsCircleContainedInMediums(Circle circle, IEnumerable<Medium> mediums)
        {
            const float pointsPerUnit = 20;
            var pointsAmount = (int)(pointsPerUnit * circle.Area);
            foreach (var point in SunflowerPoints(circle, pointsAmount))
            {
                foreach (var medium in mediums)
                {
                    if (ElementaryCollisions.IsPointInTriangle(point, medium.Triangle.ToTopDownTriangle()))
                        continue;
                }

                // if no triangles contain this point, the circle is not contained in any medium
                return false;
            }
            return true;
        }

        private static bool ThouroughlyCheckSpheresForCollision(IEnumerable<Sphere> groupA, IEnumerable<Sphere> groupB)
        {
            foreach (var a in groupA)
            {
                foreach (var b in groupB)
                {
                    if (ElementaryCollisions.AreSpheresColliding(a, b))
                        return true;
                }
            }
            return false;
        }
    }
}