// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;
using System.Numerics;

namespace HenHen.Framework.Worlds.Nodes
{
    public abstract class Node
    {
        public int Id { get; set; }
        public Vector3 Position { get; set; }

        public void Simulate(TimeSpan duration) => Simulation(duration);

        protected virtual void Simulation(TimeSpan duration)
        {
        }
    }
}