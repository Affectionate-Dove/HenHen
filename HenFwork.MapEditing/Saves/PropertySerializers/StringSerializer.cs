// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenFwork.MapEditing.Saves.PropertySerializers
{
    /// <summary>
    ///     Serializes a string so that it can be stored.
    /// </summary>
    /// <remarks>
    ///     Escapes disallowed characters, such as \r, \n and \t.
    /// </remarks>
    public class StringSerializer : SaveableMemberSerializer
    {
        protected override object DeserializeInternal(string data) => data.Replace("\\t", "\t").Replace("\\r", "\r").Replace("\\n", "\n");

        protected override string SerializeInternal(object obj) => ((string)obj).Replace("\t", "\\t").Replace("\r", "\\r").Replace("\n", "\\n");
    }
}