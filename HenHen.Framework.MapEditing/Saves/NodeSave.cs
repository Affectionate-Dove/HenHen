// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Worlds.Nodes;

namespace HenHen.Framework.MapEditing.Saves
{
    /// <summary>
    ///     A state of a <see cref="Node"/>. Supports being created from
    ///     or transformed into either the <see cref="Node"/>
    ///     or data in the form of a <see cref="string"/>.
    /// </summary>
    public record NodeSave
    {
        public NodeSave(Node node) => throw new System.NotImplementedException();

        public NodeSave(string data) => throw new System.NotImplementedException();

        public override string ToString() => throw new System.NotImplementedException();

        public Node ToNode() => throw new System.NotImplementedException();
    }
}