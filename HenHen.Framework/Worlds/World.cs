// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;
using HenHen.Framework.Worlds.Mediums;
using HenHen.Framework.Worlds.Nodes;
using System.Collections.Generic;

namespace HenHen.Framework.Worlds
{
    public class World
    {
        private readonly List<Node> nodes = new();
        public List<Medium> Mediums { get; } = new();
        public IReadOnlyList<Node> Nodes => nodes;

        public void AddNode(Node node)
        {
            nodes.Add(node);
            node.NodeEjected += OnNodeEjected;
        }

        public void Simulate(double duration)
        {
            foreach (var node in Nodes)
                node.Simulate(duration);
            CollisionChecker.CheckNodeCollisions(Nodes, OnNodesCollision);
        }

        protected virtual void OnNodesCollision(Node a, Node b)
        {
            a.OnCollision(b);
            b.OnCollision(a);
        }

        private void OnNodeEjected(Node node) => AddNode(node);
    }
}