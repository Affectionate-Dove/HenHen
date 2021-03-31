// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using NUnit.Framework;
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
    }
}