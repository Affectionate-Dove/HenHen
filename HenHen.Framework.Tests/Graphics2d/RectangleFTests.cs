using HenHen.Framework.Graphics2d;
using NUnit.Framework;
using System.Numerics;


namespace HenHen.Framework.Tests.Graphics2d
{
    public class RectangleFTests
    {
        private RectangleF rect;

        [SetUp]
        public void Setup()
        {
            rect = new RectangleF
            {
                TopLeft = new Vector2(2, 2),
                BottomRight = new Vector2(4, 4)
            };
        }

        [Test]
        public void Test()
        {
            Assert.AreEqual(new Vector2(2, 2), rect.Size);
            Assert.AreEqual(new Vector2(3), rect.Center);
        }
    }
}
