// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Numerics;
using HenFwork.MapEditing.Saves.PropertySerializers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Numerics;

namespace HenFwork.MapEditing.Tests.Saves.PropertySerializers
{
    [TestOf(typeof(SaveableMemberSerializer))]
    public class SimpleSerializersTests
    {
        public static IReadOnlyList<SimpleSerializerTest> Tests { get; } = new List<SimpleSerializerTest>()
        {
            new(new Vector2Serializer(), new object[] { new Vector2(2.1f, -4), new Vector2() }),
            new(new Vector3Serializer(), new object[] { new Vector3(-1, 4, 99.2f), new Vector3() }),
            new(new Triangle3Serializer(), new object[] { new Triangle3(new(-1, 99.2f, 4), new(), new(4)), new Triangle3() })
        };

        [TestCaseSource(nameof(Tests))]
        public void SerializationDeserializationTest(SimpleSerializerTest t)
        {
            foreach (var obj in t.Objects)
            {
                var serializedObj = t.Serializer.Serialize(obj);
                var deserializedObj = t.Serializer.Deserialize(serializedObj);
                Assert.AreEqual(obj, deserializedObj);
            }
        }

        public record SimpleSerializerTest(SaveableMemberSerializer Serializer, object[] Objects);
    }
}