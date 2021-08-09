// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Worlds;
using System.Collections.Generic;

namespace HenHen.Framework.MapEditing.Saves
{
    /// <summary>
    ///     A state of a <see cref="World"/>. Supports being
    ///     created from or transformed into either the <see cref="World"/>
    ///     or data in the form of a <see cref="string"/>.
    /// </summary>
    public record WorldSave
    {
        public IReadOnlyList<NodeSave> NodeSaves { get; }

        public WorldSave(World world) => throw new System.NotImplementedException();

        public WorldSave(string data) => throw new System.NotImplementedException();

        public override string ToString() => throw new System.NotImplementedException();

        public World ToWorld() => throw new System.NotImplementedException();
    }
}