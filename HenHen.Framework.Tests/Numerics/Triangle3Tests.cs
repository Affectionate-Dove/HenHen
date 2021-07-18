// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using NUnit.Framework;
using System.Numerics;

namespace HenHen.Framework.Tests.Numerics
{
    internal class Triangle3Tests
    {
        private readonly Triangle3 triangle = new(new(0, 0, 4), new(0, 1.5f, 5), new(3, 0, 2));

        [Test]
        public void CentroidTest() => Assert.AreEqual(new Vector3(1, 0.5f, 3 + (2 / 3f)), triangle.Centroid);

        [Test]
        public void ReversedTest() => Assert.AreEqual(new Triangle3(new(3, 0, 2), new(0, 1.5f, 5), new(0, 0, 4)), triangle.Reversed());
    }
}