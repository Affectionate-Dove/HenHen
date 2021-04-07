// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using HenHen.Framework.Tests.Collisions;
using HenHen.Framework.Worlds.Chunks;
using HenHen.Framework.Worlds.Mediums;
using HenHen.Framework.Worlds.Nodes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace HenHen.Framework.Tests.Worlds.Chunks
{
    public class ChunksManagerTests
    {
        private static IReadOnlyCollection<AddMediumTestCase> AddMediumTestCases => new List<AddMediumTestCase>
        {
            new AddMediumTestCase
            (
                new Medium
                {
                    Triangle = new Triangle3
                    {
                        A = new Vector3(0, 0, 0),
                        B = new Vector3(2, 0, 0.5f),
                        C = new Vector3(1, 0, 0.8f)
                    }
                },
                new HashSet<Vector2>
                {
                    new Vector2(0),
                    new Vector2(1, 0),
                    new Vector2(2, 0)
                }
            ),
            new AddMediumTestCase
            (
                new Medium
                {
                    Triangle = new Triangle3
                    {
                        A = new Vector3(0.5f, 0, 2.5f),
                        B = new Vector3(2.9f, 0, 1.9f),
                        C = new Vector3(1.2f, 0, 1.2f)
                    }
                },
                new HashSet<Vector2>
                {
                    new Vector2(0, 2),
                    new Vector2(1, 2),
                    new Vector2(2, 2),
                    new Vector2(0, 1),
                    new Vector2(1, 1),
                    new Vector2(2, 1)
                }
            )
        };

        private static IReadOnlyCollection<AddNodeTestCase> AddNodeTestCases => new List<AddNodeTestCase>
        {
            new AddNodeTestCase
            (
                new TestCollisionNode(1, new Vector3(1, 0, 2), new[] { new Sphere { Radius = 0.5f } }),
                new HashSet<Vector2>
                {
                    new Vector2(0, 2),
                    new Vector2(1, 2),
                    new Vector2(0, 1),
                    new Vector2(1, 1)
                }
            ),
            new AddNodeTestCase
            (
                new TestCollisionNode(2, new Vector3(3, 1, 1), new[]
                {
                    new Sphere { Radius = 0.7f },
                    new Sphere { CenterPosition = new Vector3(-0.8f, 0, 0), Radius = 0.3f }
                }),
                new HashSet<Vector2>
                {
                    new Vector2(1, 1),
                    new Vector2(2, 1),
                    new Vector2(3, 1),
                    new Vector2(1, 0),
                    new Vector2(2, 0),
                    new Vector2(3, 0),
                })
        };

        [Test]
        public void CtorTest()
        {
            const int chunk_size = 15;
            var chunkCount = new Vector2(6, 8);
            var chunksManager = new ChunksManager(chunkCount, chunk_size);
            Assert.AreEqual(chunk_size, chunksManager.ChunkSize);
            Assert.AreEqual(chunkCount.X * chunkCount.Y, chunksManager.Chunks.Count);
        }

        [TestCase(1, 0, 0, 0, 0)]
        [TestCase(1, 1, 1, 1, 1)]
        [TestCase(2, 1, 1, 0, 0)]
        [TestCase(5, 6.5f, 21, 1, 4)]
        [TestCase(3, 2.99f, 2.99f, 0, 0)]
        public void GetChunkIndexForPositionTest(float chunkSize, float posX, float posY, float expectedX, float expectedY)
        {
            var expected = new Vector2(expectedX, expectedY);
            var position = new Vector2(posX, posY);
            var chunksManager = new ChunksManager(new Vector2(10), chunkSize);
            Assert.AreEqual(expected, chunksManager.GetChunkIndexForPosition(position));
        }

        [TestCaseSource(nameof(AddMediumTestCases))]
        public void AddMediumTest(AddMediumTestCase tc)
        {
            var chunksManager = CreateChunksManager();
            chunksManager.AddMedium(tc.Medium);
            foreach ((var index, var chunk) in chunksManager.Chunks)
            {
                var chunkShouldHaveMedium = tc.ExpectedChunkIndexes.Contains(index);
                var chunkHasMedium = chunk.Mediums.Contains(tc.Medium);
                Assert.AreEqual(chunkShouldHaveMedium, chunkHasMedium, $"Chunk index: {index}");
            }
        }

        [TestCaseSource(nameof(AddNodeTestCases))]
        public void AddNodeTest(AddNodeTestCase tc)
        {
            var chunksManager = CreateChunksManager();
            chunksManager.AddNode(tc.Node);
            foreach ((var index, var chunk) in chunksManager.Chunks)
            {
                var chunkShouldHaveNode = tc.ExpectedChunkIndexes.Contains(index);
                var chunkHasNode = chunk.Nodes.Contains(tc.Node);
                Assert.AreEqual(chunkShouldHaveNode, chunkHasNode, $"Chunk index: {index}");
            }
        }

        private static ChunksManager CreateChunksManager() => new(new Vector2(10), 1);

        public record AddMediumTestCase(Medium Medium, IReadOnlySet<Vector2> ExpectedChunkIndexes);

        public record AddNodeTestCase(Node Node, IReadOnlySet<Vector2> ExpectedChunkIndexes);
    }
}