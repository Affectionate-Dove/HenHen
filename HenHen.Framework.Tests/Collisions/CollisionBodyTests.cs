// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;
using HenHen.Framework.Numerics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Tests.Collisions
{
    public class CollisionBodyTests
    {
        public static IEnumerable<(Sphere[] spheres, Sphere expectedSphere, Box expectedBox)> EnumerateTestCases()
        {
            yield return (new Sphere[]
            {
                new Sphere { Radius = 2 }
            }, new Sphere { Radius = 2 }, new Box(-2, 2, -2, 2, -2, 2));

            yield return (new Sphere[]
            {
                new Sphere { CenterPosition = new Vector3(1, 0, 0), Radius = 5 }
            }, new Sphere { Radius = 6 }, new Box(-4, 6, -5, 5, -5, 5));

            yield return (new Sphere[]
            {
                new Sphere { CenterPosition = new Vector3(0, 2, 0), Radius = 5 },
                new Sphere { CenterPosition = new Vector3(0, -4, 0), Radius = 5 }
            }, new Sphere { Radius = 9 }, new Box(-5, 5, -9, 7, -5, 5));

            yield return (Array.Empty<Sphere>(), new Sphere(), new Box());
        }

        [TestCaseSource(nameof(EnumerateTestCases))]
        public void ContainingSphereTest((Sphere[] spheres, Sphere expectedSphere, Box expectedBox) testCase)
        {
            if (testCase.spheres.Length == 0)
                Assert.Throws<ArgumentException>(() => new CollisionBody(testCase.spheres));
            else
            {
                var collisionBody = new CollisionBody(testCase.spheres);
                Assert.AreEqual(testCase.expectedSphere, collisionBody.ContainingSphere);
                Assert.AreEqual(testCase.expectedBox, collisionBody.BoundingBox);
            }
        }
    }
}