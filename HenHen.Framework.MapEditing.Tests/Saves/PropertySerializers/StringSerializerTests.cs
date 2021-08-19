// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.MapEditing.Saves.PropertySerializers;
using NUnit.Framework;

namespace HenHen.Framework.MapEditing.Tests.Saves.PropertySerializers
{
    [TestOf(typeof(StringSerializer))]
    public class StringSerializerTests
    {
        [TestCase("")]
        [TestCase("Sample text.")]
        [TestCase("  Sample  text. ")]
        [TestCase("\tSample  text. ")]
        [TestCase("\tSample \n text. ")]
        [TestCase("Sample \n text. ")]
        [TestCase("Sam\rple \n text. ")]
        public void Test(string sourceString)
        {
            var serializer = new StringSerializer();

            var serializedString = serializer.Serialize(sourceString);
            var deserializedString = serializer.Deserialize(serializedString);

            Assert.AreEqual(sourceString, deserializedString);
        }
    }
}