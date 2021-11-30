// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenFwork.Extensions;
using NUnit.Framework;
using System.Numerics;

namespace HenFwork.Tests.Extensions
{
    public class VectorExtensionsTests
    {
        [Test]
        public void GetAngleVector2Test()
        {
            var v = new Vector2(0, 1);
            Assert.AreEqual(180, v.GetAngle());

            v = new Vector2(1, 1);
            Assert.AreEqual(135, v.GetAngle());

            v = new Vector2(0, -1);
            Assert.AreEqual(0, v.GetAngle());

            v = new Vector2(-1, -1);
            Assert.AreEqual(315, v.GetAngle());

            v = new Vector2(-1, 0);
            Assert.AreEqual(270, v.GetAngle());

            v = new Vector2(1, 0);
            Assert.AreEqual(90, v.GetAngle());
        }

        [Test]
        public void GetRotatedVector2Test()
        {
            var v = new Vector2(0, -1);
            AreApproximatelyEqual(new Vector2(1, 0), v.GetRotated(90));
            AreApproximatelyEqual(new Vector2(0, 1), v.GetRotated(180));
            AreApproximatelyEqual(new Vector2(-1, 0), v.GetRotated(270));
            AreApproximatelyEqual(v, v.GetRotated(360));
        }

        [Test]
        public void GetRotatedVector3YTest()
        {
            var v = new Vector3(0, 1, 1);
            AreApproximatelyEqual(new Vector3(1, 1, 0), v.GetRotated(new Vector3(0, 90, 0)));
            AreApproximatelyEqual(new Vector3(0, 1, -1), v.GetRotated(new Vector3(0, 180, 0)));
            AreApproximatelyEqual(new Vector3(-1, 1, 0), v.GetRotated(new Vector3(0, 270, 0)));
            AreApproximatelyEqual(v, v.GetRotated(new Vector3(0, 360, 0)));
        }

        [Test]
        public void GetRotatedVector3ZTest()
        {
            var v = new Vector3(0, 1, 1);
            AreApproximatelyEqual(new Vector3(1, 0, 1), v.GetRotated(new Vector3(0, 0, 90)));
            AreApproximatelyEqual(new Vector3(0, -1, 1), v.GetRotated(new Vector3(0, 0, 180)));
            AreApproximatelyEqual(new Vector3(-1, 0, 1), v.GetRotated(new Vector3(0, 0, 270)));
            AreApproximatelyEqual(v, v.GetRotated(new Vector3(0, 0, 360)));
        }

        [Test]
        public void GetRotatedVector3XTest()
        {
            var v = new Vector3(1, 1, 0);
            AreApproximatelyEqual(new Vector3(1, 0, -1), v.GetRotated(new Vector3(90, 0, 0)));
            AreApproximatelyEqual(new Vector3(1, -1, 0), v.GetRotated(new Vector3(180, 0, 0)));
            AreApproximatelyEqual(new Vector3(1, 0, 1), v.GetRotated(new Vector3(270, 0, 0)));
            AreApproximatelyEqual(v, v.GetRotated(new Vector3(360, 0, 0)));
        }

        //[Test]
        //public void GetRotatedVector3YZTest()
        //{
        //    var v = new Vector3(0, 1, 0);
        //    AreApproximatelyEqual(new Vector3(0, 0, 1), v.GetRotated(new Vector3(0, 90, 90)));
        //    AreApproximatelyEqual(v, v.GetRotated(new Vector3(360, 360, 0)));
        //}

        private static void AreApproximatelyEqual(Vector2 expected, Vector2 actual)
        {
            if ((expected - actual).LengthSquared() < 0.0000001f)
                Assert.Pass();
            else
                Assert.AreEqual(expected, actual);
        }

        private static void AreApproximatelyEqual(Vector3 expected, Vector3 actual)
        {
            if ((expected - actual).Length() < 1E-5)
                Assert.Pass();
            else
                Assert.AreEqual(expected, actual);
        }
    }
}