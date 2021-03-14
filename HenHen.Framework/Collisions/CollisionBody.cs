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

        public IReadOnlyCollection<Sphere> Spheres => spheres;

        public CollisionBody(IEnumerable<Sphere> spheres)
        {
            this.spheres = spheres.ToArray();
            if (this.spheres.Length == 0)
                throw new ArgumentException($"Must contain at least 1 sphere to create a {nameof(CollisionBody)}", nameof(spheres));
            ContainingSphere = new Sphere { Radius = GetFarthestDistanceFromCenter(spheres) };
        }

        private static float GetFarthestDistanceFromCenter(IEnumerable<Sphere> spheres)
        {
            var maxDistance = 0f;
            foreach (var sphere in spheres)
                maxDistance = Math.Max(maxDistance, sphere.CenterPosition.Length() + sphere.Radius);
            return maxDistance;
        }

        public override string ToString() => $"{{CollisionBody: {Spheres}}}";
    }
}