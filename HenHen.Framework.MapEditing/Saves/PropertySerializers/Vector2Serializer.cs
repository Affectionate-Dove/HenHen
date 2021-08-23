// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenHen.Framework.MapEditing.Saves.PropertySerializers
{
    public class Vector2Serializer : SaveableMemberSerializer
    {
        protected override object DeserializeInternal(string data)
        {
            var coordinatesStrings = data.Split(',');
            var x = float.Parse(coordinatesStrings[0]);
            var y = float.Parse(coordinatesStrings[1]);
            return new Vector2(x, y);
        }

        protected override string SerializeInternal(object obj)
        {
            var v = (Vector2)obj;
            return $"{v.X},{v.Y}";
        }
    }
}