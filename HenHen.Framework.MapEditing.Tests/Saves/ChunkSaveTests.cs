// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.MapEditing.Saves;
using HenHen.Framework.Worlds.Mediums;
using NUnit.Framework;

namespace HenHen.Framework.MapEditing.Tests.Saves
{
    [TestOf(typeof(ChunkSave))]
    public class ChunkSaveTests
    {
        private NodeSave nodeSave1;
        private NodeSave nodeSave2;
        private NodeSave[] nodeSaves;
        private MediumSave[] mediumSaves;
        private (int x, int y) index;
        private int size;
        private MediumSave mediumSave1;
        private MediumSave mediumSave2;

        [SetUp]
        public void SetUp()
        {
            var nodesSerializer = new NodesSerializer();
            var node1 = new TestNodeForSaving { TestStringField = "node1" };
            var node2 = new TestNodeForSaving { TestStringProperty = "node2" };
            nodeSave1 = nodesSerializer.Serialize(node1);
            nodeSave2 = nodesSerializer.Serialize(node2);
            nodeSaves = new[] { nodeSave1, nodeSave2 };
            mediumSave1 = new MediumSave(new(new(1, 0, 0), new(0, 0, 0), new(0, 0, 1)), MediumType.Ground);
            mediumSave2 = new MediumSave(new(new(1, 3, 4), new(0, 2, 5), new(0, 1, 1)), MediumType.Air);
            mediumSaves = new[] { mediumSave1, mediumSave2 };
            index = (3, 2);
            size = 10;
        }

        [Test]
        public void FromPropertiesTest()
        {
            var chunkSave = new ChunkSave(nodeSaves, mediumSaves, index, size);
            ValidateProperties(chunkSave);
        }

        [Test]
        public void FromDataStringTest()
        {
            var dataString = new ChunkSave(nodeSaves, mediumSaves, index, size).ToDataString();
            var chunkSave = new ChunkSave(dataString);
            ValidateProperties(chunkSave);
        }

        private void ValidateProperties(ChunkSave chunkSave)
        {
            for (var i = 0; i < nodeSaves.Length; i++)
                Assert.AreEqual(nodeSaves[i].ToStringData(), chunkSave.NodeSaves[i].ToStringData());

            for (var i = 0; i < mediumSaves.Length; i++)
                Assert.AreEqual(mediumSaves[i], chunkSave.MediumSaves[i]);

            Assert.AreEqual(index, chunkSave.Index);
            Assert.AreEqual(size, chunkSave.Size);
        }
    }
}