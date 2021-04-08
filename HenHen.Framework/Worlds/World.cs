// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Worlds.Mediums;
using HenHen.Framework.Worlds.Nodes;
using System;
using System.Collections.Generic;

namespace HenHen.Framework.Worlds
{
    public class World
    {
        public List<Medium> Mediums { get; } = new();
        public List<Node> Nodes { get; } = new();

        public void Simulate(TimeSpan duration)
        {
            foreach (var node in Nodes)
                node.Simulate(duration);
        }
    }
}