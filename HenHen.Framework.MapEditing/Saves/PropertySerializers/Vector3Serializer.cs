// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenHen.Framework.MapEditing.Saves.PropertySerializers
{
    public class Vector3Serializer : SaveableMemberSerializer
    {
        protected override object DeserializeInternal(string data)
        {
            var coordinatesStrings = data.Split(',');
            var x = float.Parse(coordinatesStrings[0]);
            var y = float.Parse(coordinatesStrings[1]);
            var z = float.Parse(coordinatesStrings[2]);
            return new Vector3(x, y, z);
        }

        protected override string SerializeInternal(object obj)
        {
            var v = (Vector3)obj;
            return $"{v.X},{v.Y},{v.Z}";
        }
    }
}