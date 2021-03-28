using HenHen.Framework.Worlds.PathFinding;
using NUnit.Framework;

namespace HenHen.Framework.Tests.Worlds.PathFinding
{
    internal class PathNodeTests
    {
        [Test]
        public void ConnectSymmetricallyTest()
        {
            PathNode a = new PathNode();
            PathNode b = new PathNode();
            a.ConnectSymmetrically(b);
            Assert.IsTrue(a.Connections.Contains(b));
            Assert.IsTrue(b.Connections.Contains(a));
        }

        [Test]
        public void ConnectAsymmetricallyTest()
        {
            PathNode a = new PathNode();
            PathNode b = new PathNode();
            a.ConnectAsymmetrically(b);
            Assert.IsTrue(a.Connections.Contains(b));
            Assert.IsFalse(b.Connections.Contains(a));
        }

        [Test]
        public void DisconnectSimmetricallyTest()
        {
            PathNode a = new PathNode();
            PathNode b = new PathNode();
            a.ConnectSymmetrically(b);
            Assert.IsTrue(a.Connections.Contains(b));
            Assert.IsTrue(b.Connections.Contains(a));
            a.DisconnectSymmetrically(b);
            Assert.IsFalse(a.Connections.Contains(b));
            Assert.IsFalse(b.Connections.Contains(a));
        }

        [Test]
        public void DisconnectAsimmetricallyTest()
        {
            PathNode a = new PathNode();
            PathNode b = new PathNode();
            a.ConnectSymmetrically(b);
            Assert.IsTrue(a.Connections.Contains(b));
            Assert.IsTrue(b.Connections.Contains(a));
            a.DisconnectAsymmetrically(b);
            Assert.IsFalse(a.Connections.Contains(b));
            Assert.IsTrue(b.Connections.Contains(a));
        }
    }
}