// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;
using System.Numerics;

namespace HenHen.Framework.Worlds.Nodes
{
    public abstract class Node
    {
        public CollisionBody CollisionBody { get; set; }
        public int Id { get; set; }
        public Vector3 Position { get; set; }
        public object SynchronizedTime { get; private set; }

        public virtual void OnCollision(Node a)
        {
        }

        public void Simulate(object newTime)
        {
            Simulation(newTime/* - SynchronizedTime*/);
            SynchronizedTime = newTime;
        }

        public override string ToString() => $"{GetType().Name}:{{" +
            $"{nameof(Id)}:{Id}," +
            $"{nameof(Position)}:{Position}," +
            $"{nameof(CollisionBody)}:{CollisionBody}" +
            $"}}";

        protected virtual void Simulation(object duration)
        {
        }
    }
}