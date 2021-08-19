// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.MapEditing.Saves.PropertySerializers;
using NUnit.Framework;

namespace HenHen.Framework.MapEditing.Tests.Saves.PropertySerializers
{
    [TestOf(typeof(SaveableMemberSerializer))]
    public class SaveableMemberSerializerTests
    {
        private BadStringSerializer badStringSerializer;

        [SetUp]
        public void SetUp() => badStringSerializer = new BadStringSerializer();

        [TestCase("Here is a tabulator: \t.")]
        [TestCase("Here is a carriage return: \r.")]
        [TestCase("Here is a newline: \n.")]
        public void ThrowOnIllegalCharactersTest(string s) => Assert.Throws<System.FormatException>(() => badStringSerializer.Serialize(s));

        [Test]
        public void SerializationNullExceptionTest() => Assert.Throws<System.ArgumentNullException>(() => badStringSerializer.Serialize(null));

        [Test]
        public void DeserializationNullExceptionTest() => Assert.Throws<System.ArgumentNullException>(() => badStringSerializer.Deserialize(null));

        public class BadStringSerializer : SaveableMemberSerializer
        {
            protected override object DeserializeInternal(string data) => data;

            protected override string SerializeInternal(object obj) => (string)obj;
        }
    }
}