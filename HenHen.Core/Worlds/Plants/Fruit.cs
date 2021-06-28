// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Worlds.Nodes;

namespace HenHen.Core.Worlds.Plants
{
    public class Fruit : Node
    {
        public string Name { get; }

        public Fruit(string name) => Name = name;
    }
}