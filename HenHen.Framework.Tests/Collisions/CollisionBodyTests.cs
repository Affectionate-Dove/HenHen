// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;
using HenHen.Framework.Numerics;
using NUnit.Framework;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Tests.Collisions
{
    public class CollisionBodyTests
    {
        public static IEnumerable<(Sphere[] spheres, Sphere expected)> EnumerateTestCases()
        {
            yield return (new Sphere[]
            {
                new Sphere { Radius = 2 }
            }, new Sphere { Radius = 2 });

            yield return (new Sphere[]
            {
                new Sphere { CenterPosition = new Vector3(1, 0, 0), Radius = 5 }
            }, new Sphere { Radius = 6 });

            yield return (new Sphere[]
            {
                new Sphere { CenterPosition = new Vector3(0, 2, 0), Radius = 5 },
                new Sphere { CenterPosition = new Vector3(0, -4, 0), Radius = 5 }
            }, new Sphere { Radius = 9 });

            yield return (System.Array.Empty<Sphere>(), new Sphere());
        }

        [TestCaseSource(nameof(EnumerateTestCases))]
        public void ContainingSphereTest((Sphere[] spheres, Sphere expected) testCase)
        {
            var collisionBody = new CollisionBody(testCase.spheres);
            Assert.AreEqual(testCase.expected, collisionBody.ContainingSphere);
        }
    }
}