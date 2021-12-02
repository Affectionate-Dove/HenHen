// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Numerics;
using System.Numerics;

namespace HenFwork.MapEditing.Saves.PropertySerializers
{
    public class Triangle3Serializer : SaveableMemberSerializer
    {
        private const char vertices_separator = ';';
        private static readonly Vector3Serializer vector3Serializer = new();

        protected override object DeserializeInternal(string data)
        {
            var verticesStrings = data.Split(vertices_separator);
            var vertexA = (Vector3)vector3Serializer.Deserialize(verticesStrings[0]);
            var vertexB = (Vector3)vector3Serializer.Deserialize(verticesStrings[1]);
            var vertexC = (Vector3)vector3Serializer.Deserialize(verticesStrings[2]);
            return new Triangle3(vertexA, vertexB, vertexC);
        }

        protected override string SerializeInternal(object obj)
        {
            var triangle = (Triangle3)obj;
            var serializedVertexA = vector3Serializer.Serialize(triangle.A);
            var serializedVertexB = vector3Serializer.Serialize(triangle.B);
            var serializedVertexC = vector3Serializer.Serialize(triangle.C);
            return serializedVertexA + vertices_separator + serializedVertexB + vertices_separator + serializedVertexC;
        }
    }
}