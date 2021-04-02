// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using HenHen.Framework.Tests.Collisions;
using HenHen.Framework.Worlds.Chunks;
using HenHen.Framework.Worlds.Mediums;
using NUnit.Framework;
using System;
using System.Linq;
using System.Numerics;

namespace HenHen.Framework.Tests.Worlds.Chunks
{
    public class ChunkTests
    {
        [Test]
        public void IndexAndCoordinatesTest()
        {
            var index = new Vector2(4, 2);
            var chunkSize = 64;
            var expectedRect = new RectangleF
            {
                Left = 256,
                Right = 256 + chunkSize,
                Bottom = 128,
                Top = 128 + chunkSize
            };
            var chunk = new Chunk(index, chunkSize);
            Assert.AreEqual(index, chunk.Index);
            Assert.AreEqual(expectedRect, chunk.Coordinates);
        }

        [Test]
        public void NodeAdditionAndRemovalTest()
        {
            var node = CreateNode();
            var chunk = CreateChunk();
            Assert.IsTrue(chunk.Nodes.Count == 0);
            chunk.AddNode(node);
            Assert.IsTrue(chunk.Nodes.Contains(node));
            chunk.RemoveNode(node);
            Assert.IsTrue(chunk.Nodes.Count == 0);
        }

        [Test]
        public void MediumAdditionAndRemovalTest()
        {
            var medium = CreateMedium();
            var chunk = CreateChunk();
            Assert.IsTrue(chunk.Mediums.Count == 0);
            chunk.AddMedium(medium);
            Assert.IsTrue(chunk.Mediums.Contains(medium));
            chunk.RemoveMedium(medium);
            Assert.IsTrue(chunk.Mediums.Count == 0);
        }

        [Test]
        public void SimulateChunkTest()
        {
            var chunk = CreateChunk();
            chunk.AddMedium(CreateMedium());
            var node = CreateNode();
            chunk.AddNode(node);
            Assert.IsTrue(chunk.Nodes.Contains(node));
            Assert.IsTrue(!chunk.Simulate(null).Contains(node));
            node.Position = new Vector3();
            Assert.IsTrue(chunk.Simulate(null).Contains(node));
        }

        private static Medium CreateMedium() => new()
        {
            Triangle = new Triangle3
            {
                A = new Vector3(258, 0, 128),
                B = new Vector3(258, 0, 140),
                C = new Vector3(280, 0, 128)
            }
        };

        private static TestCollisionNode CreateNode() => new(1, new Vector3(260, 0, 130), new[]
        {
            new Sphere { Radius = 1 }
        });

        private static Chunk CreateChunk() => new(new Vector2(4, 2), 64);
    }
}