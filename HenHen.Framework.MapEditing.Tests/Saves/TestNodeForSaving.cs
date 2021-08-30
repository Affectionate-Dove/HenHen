// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.MapEditing.Saves;
using HenHen.Framework.Worlds.Nodes;

namespace HenHen.Framework.MapEditing.Tests.Saves
{
    public class TestNodeForSaving : Node
    {
        [Saveable]
        private string testStringField;

        public string TestStringField
        {
            get => testStringField;
            set => testStringField = value;
        }

        [Saveable]
        public string TestStringProperty { get; set; }
    }
}