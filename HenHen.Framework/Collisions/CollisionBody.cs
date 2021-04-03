// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HenHen.Framework.Collisions
{
    public record CollisionBody
    {
        private readonly Sphere[] spheres;

        public Sphere ContainingSphere { get; }
        public Box BoundingBox { get; }

        public IReadOnlyCollection<Sphere> Spheres => spheres;

        public CollisionBody(IEnumerable<Sphere> spheres)
        {
            this.spheres = spheres.ToArray();
            if (this.spheres.Length == 0)
                throw new ArgumentException($"Must contain at least 1 sphere to create a {nameof(CollisionBody)}", nameof(spheres));
            ContainingSphere = new Sphere { Radius = GetFarthestDistanceFromCenter(spheres) };
            BoundingBox = CreateBoundingBox(spheres);
        }

        private static Box CreateBoundingBox(IEnumerable<Sphere> spheres)
        {
            var box = new Box();
            foreach (var sphere in spheres)
            {
                box.Left = Math.Min(box.Left, sphere.CenterPosition.X - sphere.Radius);
                box.Right = Math.Max(box.Right, sphere.CenterPosition.X + sphere.Radius);
                box.Bottom = Math.Min(box.Bottom, sphere.CenterPosition.Y - sphere.Radius);
                box.Top = Math.Max(box.Top, sphere.CenterPosition.Y + sphere.Radius);
                box.Back = Math.Min(box.Back, sphere.CenterPosition.Z - sphere.Radius);
                box.Front = Math.Max(box.Front, sphere.CenterPosition.Z + sphere.Radius);
            }
            return box;
        }

        private static float GetFarthestDistanceFromCenter(IEnumerable<Sphere> spheres)
        {
            var maxDistance = 0f;
            foreach (var sphere in spheres)
                maxDistance = Math.Max(maxDistance, sphere.CenterPosition.Length() + sphere.Radius);
            return maxDistance;
        }

        public override string ToString() => $"{{{nameof(Spheres)}: {string.Join(',', spheres)}}}";
    }
}