// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;
using System;
using System.Numerics;

namespace HenHen.Framework.Worlds.Nodes
{
    public abstract class Node
    {
        public event Action<Node> NodeEjected;

        public CollisionBody CollisionBody { get; set; }
        public int Id { get; set; }
        public Vector3 Position { get; set; }

        public double SynchronizedTime { get; private set; }

        public virtual void OnCollision(Node a)
        {
        }

        public void Simulate(double newTime)
        {
            Simulation(newTime - SynchronizedTime);
            SynchronizedTime = newTime;
        }

        public override string ToString() => $"{GetType().Name}:{{" +
            $"{nameof(Id)}:{Id}," +
            $"{nameof(Position)}:{Position}," +
            $"{nameof(CollisionBody)}:{CollisionBody}" +
            $"}}";

        protected void EjectNode(Node node) => NodeEjected?.Invoke(node);

        protected virtual void Simulation(double duration)
        {
        }
    }
}