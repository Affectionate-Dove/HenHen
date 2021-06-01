// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using NUnit.Framework;
using System;
using System.Numerics;

namespace HenHen.Framework.Tests.Numerics
{
    public class RectangleFTests
    {
        private RectangleF rect;

        [Test]
        public void Test()
        {
            rect = new RectangleF
            {
                TopLeft = new Vector2(2, 2),
                BottomRight = new Vector2(4, 4)
            };

            Assert.AreEqual(new Vector2(2, 2), rect.Size);
            Assert.AreEqual(new Vector2(3), rect.Center);
            Assert.AreEqual(4, rect.Area);
        }

        [Test]
        public void Test2()
        {
            rect = new RectangleF
            {
                Top = -2,
                Left = -4,
                Bottom = 3,
                Right = -6
            };

            Assert.AreEqual(-2, rect.Width);
            Assert.AreEqual(5, rect.Height);
            Assert.AreEqual(10, rect.Area);
        }

        [Test]
        public void Test3()
        {
            rect = new RectangleF
            {
                Top = -4,
                Left = 3,
                Bottom = -6,
                Right = -2
            };

            Assert.AreEqual(-5, rect.Width);
            Assert.AreEqual(-2, rect.Height);
            Assert.AreEqual(10, rect.Area);
        }

        [Test]
        public void Test4()
        {
            rect = new RectangleF
            {
                Top = -2,
                Left = -4,
                Height = 5,
                Width = -2
            };

            Assert.AreEqual(-2, rect.Width);
            Assert.AreEqual(5, rect.Height);
            Assert.AreEqual(10, rect.Area);
            Assert.AreEqual(3, rect.Bottom);
            Assert.AreEqual(-6, rect.Right);
        }

        [Test]
        public void GetIntersectionTest()
        {
            var rect1 = new RectangleF(1, 2, 1, 2);
            var rect2 = new RectangleF(1.5f, 2.5f, 1.5f, 2.5f);
            var rect1YD = new RectangleF(1, 2, 2, 1);
            var rect2YD = new RectangleF(1.5f, 2.5f, 2.5f, 1.5f);
            var rect3 = new RectangleF(3, 4, 3, 4);

            Assert.Throws<Exception>(() => rect1.GetIntersection(rect1YD));
            Assert.Throws<Exception>(() => rect1.GetIntersection(rect2YD));
            Assert.Throws<Exception>(() => rect2.GetIntersection(rect1YD));
            Assert.Throws<Exception>(() => rect2.GetIntersection(rect2YD));

            RectangleF? expected = new RectangleF(1.5f, 2, 1.5f, 2);
            Assert.AreEqual(expected, rect1.GetIntersection(rect2));
            Assert.AreEqual(expected, rect2.GetIntersection(rect1));

            expected = new RectangleF(1.5f, 2, 2, 1.5f);
            Assert.AreEqual(expected, rect1YD.GetIntersection(rect2YD));
            Assert.AreEqual(expected, rect2YD.GetIntersection(rect1YD));

            expected = null;
            Assert.AreEqual(expected, rect1.GetIntersection(rect3));
            Assert.AreEqual(expected, rect3.GetIntersection(rect1));
        }
    }
}