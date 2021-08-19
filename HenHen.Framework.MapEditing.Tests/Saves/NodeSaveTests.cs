// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.MapEditing.Saves;
using NUnit.Framework;

namespace HenHen.Framework.MapEditing.Tests.Saves
{
    [TestFixture(TestOf = typeof(NodeSave))]
    public class NodeSaveTests
    {
        private const string test_data = "HenHen.Framework.MapEditing.Tests|Saves.TestNodeForSaving\ttestStringField:hy\tTestStringProperty:da";

        private NodeSave nodeSave;

        [SetUp]
        public void SetUp() => nodeSave = new NodeSave(test_data);

        [Test(Description = "Tests whether the object created from a data string has proper member values.")]
        public void FromDataTest()
        {
            Assert.AreEqual("HenHen.Framework.MapEditing.Tests", nodeSave.AssemblyName);
            Assert.AreEqual("Saves.TestNodeForSaving", nodeSave.TypeName);
            Assert.AreEqual("HenHen.Framework.MapEditing.Tests.Saves.TestNodeForSaving", nodeSave.FullTypeName);
            Assert.AreEqual("hy", nodeSave.MembersValues["testStringField"]);
            Assert.AreEqual("da", nodeSave.MembersValues["TestStringProperty"]);
        }

        [Test(Description = "Whether the serialization returns a string equal to one that the object was created from.")]
        public void ToStringDataTest() => Assert.AreEqual(test_data, nodeSave.ToStringData());
    }
}