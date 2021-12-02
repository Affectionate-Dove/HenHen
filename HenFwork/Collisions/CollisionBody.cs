// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HenFwork.Collisions
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
            float? left = null, right = null, bottom = null, top = null, back = null, front = null;

            static float assign(float? oldValue, float newValue, Func<float, float, float> func) => oldValue.HasValue ? func(oldValue.Value, newValue) : newValue;

            foreach (var sphere in spheres)
            {
                left = assign(left, sphere.CenterPosition.X - sphere.Radius, Math.Min);
                right = assign(right, sphere.CenterPosition.X + sphere.Radius, Math.Max);
                bottom = assign(bottom, sphere.CenterPosition.Y - sphere.Radius, Math.Min);
                top = assign(top, sphere.CenterPosition.Y + sphere.Radius, Math.Max);
                back = assign(back, sphere.CenterPosition.Z - sphere.Radius, Math.Min);
                front = assign(front, sphere.CenterPosition.Z + sphere.Radius, Math.Max);
            }

            return new(left.Value, right.Value, bottom.Value, top.Value, back.Value, front.Value);
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