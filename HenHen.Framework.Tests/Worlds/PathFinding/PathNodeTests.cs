using HenHen.Framework.Worlds.PathFinding;
using NUnit.Framework;

namespace HenHen.Framework.Tests.Worlds.PathFinding
{
    public class PathNodeTests
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
        public void DisconnectSymmetricallyTest()
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
        public void DisconnectAsymmetricallyTest()
        {
            PathNode a = new PathNode();
            PathNode b = new PathNode();
            a.ConnectSymmetrically(b);
            Assert.IsTrue(a.Connections.Contains(b));
            Assert.IsTrue(b.Connections.Contains(a));
            a.DisconnectAsymmetrically(b);
            Assert.IsFalse(a.Connections.Contains(b));
            Assert.IsTrue(b.Connections.Contains(a));
            b.ConnectSymmetrically(a);
            Assert.IsTrue(b.Connections.Contains(a));
            Assert.IsTrue(a.Connections.Contains(b));
            b.DisconnectAsymmetrically(a);
            Assert.IsFalse(b.Connections.Contains(a));
            Assert.IsTrue(a.Connections.Contains(b));
        }
    }
}