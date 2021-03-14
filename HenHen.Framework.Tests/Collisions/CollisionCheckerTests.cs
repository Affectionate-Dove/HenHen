// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using NUnit.Framework;

namespace HenHen.Framework.Tests.Collisions
{
    public class CollisionCheckerTests
    {
        [Test]
        public void CheckNodeCollisionsTest() => Assert.Inconclusive("Test not implemented");

        //{
        //    var a = new TestCollisionNode(new Vector3(), new Sphere[]
        //    {
        //        new Sphere { Radius = 1 }
        //    });
        //    var b = new TestCollisionNode(new Vector3(1, 0, 0), new Sphere[]
        //    {
        //        new Sphere { Radius = 2 }
        //    });

        //    var nodes = new List<Node>
        //    {
        //        a, b
        //    };

        //    IReadOnlyCollection<(Node, Node)> expectedCollisions = new List<(Node, Node)>
        //    {
        //        (a, b)
        //    };

        //    CollisionChecker.CheckNodeCollisions(nodes, TestCollisionHandler);
        //}

        [Test]
        public void IsNodeContainedInMediumsTest() => Assert.Inconclusive("Test not implemented");
    }
}