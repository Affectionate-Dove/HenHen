// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenFwork.MapEditing.Saves.PropertySerializers
{
    public abstract class SaveableMemberSerializer
    {
        public object Deserialize(string data)
        {
            if (data is null)
                throw new System.ArgumentNullException(nameof(data));

            return DeserializeInternal(data);
        }

        public string Serialize(object obj)
        {
            if (obj is null)
                throw new System.ArgumentNullException(nameof(obj));

            var s = SerializeInternal(obj);

            var indexOfUnallowedCharacter = s.IndexOfAny(new[] { '\t', '\n', '\r' });
            if (indexOfUnallowedCharacter >= 0)
                throw new System.FormatException(@$"The serialized string contains an unallowed character at index {indexOfUnallowedCharacter}. An unallowed character is a tabulator (\t), carriage return (\r), or a newline (\n).");

            return s;
        }

        protected abstract object DeserializeInternal(string data);

        protected abstract string SerializeInternal(object obj);
    }
}