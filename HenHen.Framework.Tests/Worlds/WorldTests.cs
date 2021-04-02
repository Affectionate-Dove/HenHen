// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using HenHen.Framework.Tests.Collisions;
using HenHen.Framework.Worlds;
using HenHen.Framework.Worlds.Mediums;
using NUnit.Framework;
using System;
using System.Numerics;

namespace HenHen.Framework.Tests.Worlds
{
    public class WorldTests
    {
        [Test]
        public void OnNodesCollisionTest()
        {
            var world = new World();
            var node1 = new TestCollisionNode(1, new Vector3(), new[] { new Sphere { Radius = 2 } });
            world.Nodes.Add(node1);
            var node2 = new TestCollisionNode(2, new Vector3(), new[] { new Sphere { Radius = 1 } });
            world.Nodes.Add(node2);
            var medium = new Medium
            {
                Triangle = new Triangle3
                {
                    A = new Vector3(-10, 0, -10),
                    B = new Vector3(0, 0, 10),
                    C = new Vector3(10, 0, -10)
                }
            };
            world.Mediums.Add(medium);
            world.Simulate(TimeSpan.FromSeconds(0.01));
            Assert.IsTrue(node1.CollisionRecord.Contains(node2));
            Assert.IsTrue(node2.CollisionRecord.Contains(node1));
        }
    }
}