// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.MapEditing.Saves;
using HenHen.Framework.Worlds.Mediums;
using NUnit.Framework;
using System.Collections.Generic;

namespace HenHen.Framework.MapEditing.Tests.Saves
{
    [TestOf(typeof(WorldSave))]
    public class WorldSaveTests
    {
        private ChunkSave[] chunks;

        [SetUp]
        public void SetUp()
        {
            var nodesSerializer = new NodesSerializer();
            var node1 = new TestNodeForSaving { TestStringField = "node1" };
            var node2 = new TestNodeForSaving { TestStringProperty = "node2" };
            var node3 = new TestNodeForSaving { TestStringField = "node3" };
            var node4 = new TestNodeForSaving { TestStringProperty = "node4" };
            var nodeSave1 = nodesSerializer.Serialize(node1);
            var nodeSave2 = nodesSerializer.Serialize(node2);
            var nodeSave3 = nodesSerializer.Serialize(node3);
            var nodeSave4 = nodesSerializer.Serialize(node4);

            var nodeSaves1 = new[] { nodeSave1, nodeSave2 };
            var nodeSaves2 = new[] { nodeSave3 };
            var nodeSaves3 = new[] { nodeSave4 };
            var nodeSaves4 = System.Array.Empty<NodeSave>();

            var mediumSave1 = new MediumSave(new(new(1, 0, 0), new(0, 0, 0), new(0, 0, 1)), MediumType.Ground);
            var mediumSave2 = new MediumSave(new(new(1, 3, 4), new(0, 2, 5), new(0, 1, 1)), MediumType.Air);
            var mediumSaves = new[] { mediumSave1, mediumSave2 };

            var chunkSizes = 10;
            var chunk1 = new ChunkSave(nodeSaves1, mediumSaves, (0, 0), chunkSizes);
            var chunk2 = new ChunkSave(nodeSaves2, new List<MediumSave>(), (0, 1), chunkSizes);
            var chunk3 = new ChunkSave(nodeSaves3, new List<MediumSave>(), (1, 0), chunkSizes);
            var chunk4 = new ChunkSave(nodeSaves4, new List<MediumSave>(), (1, 1), chunkSizes);
            chunks = new[] { chunk1, chunk2, chunk3, chunk4 };
        }

        [Test]
        public void FromPropertiesTest()
        {
            var worldSave = new WorldSave(chunks);
            ValidateProperties(worldSave);
        }

        [Test]
        public void FromDataStringTest()
        {
            var dataString = new WorldSave(chunks).ToDataString();
            var worldSave = new WorldSave(dataString);
            ValidateProperties(worldSave);
        }

        private void ValidateProperties(WorldSave worldSave)
        {
            for (var i = 0; i < chunks.Length; i++)
                Assert.AreEqual(chunks[i].ToDataString(), worldSave.ChunkSaves[i].ToDataString());
        }
    }
}