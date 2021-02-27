using HenHen.Framework.Extensions;
using NUnit.Framework;
using System.Numerics;

namespace HenHen.Framework.Tests.Extensions
{
    public class Vector2ExtensionsTests
    {
        [Test]
        public void GetAngleTest()
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
        public void GetRotatedTest()
        {
            var v = new Vector2(0, -1);
            AreApproximatelyEqual(new Vector2(1, 0), v.GetRotated(90));
            AreApproximatelyEqual(new Vector2(0, 1), v.GetRotated(180));
            AreApproximatelyEqual(new Vector2(-1, 0), v.GetRotated(270));
            AreApproximatelyEqual(v, v.GetRotated(360));
        }

        private static void AreApproximatelyEqual(Vector2 expected, Vector2 actual)
        {
            if ((expected - actual).LengthSquared() < 0.001f)
                Assert.Pass();
            else
                Assert.AreEqual(expected, actual);
        }
    }
}