﻿using HenHen.Framework.Graphics2d;
using NUnit.Framework;
using System.Numerics;

namespace HenHen.Framework.Tests.Graphics2d
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
        }

        [Test]
        public void Test2()
        {
            rect = new RectangleF
            {
                Top = 3,
                Left = -4,
                Bottom = -2,
                Right = -6
            };
            Assert.AreEqual(-2, rect.Width);
            Assert.AreEqual(-5, rect.Height);
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
    }
}
