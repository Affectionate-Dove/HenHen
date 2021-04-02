// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;
using HenHen.Framework.Extensions;
using HenHen.Framework.Numerics;
using HenHen.Framework.Worlds.Mediums;
using HenHen.Framework.Worlds.Nodes;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Worlds.Chunks
{
    /// <summary>
    /// Represents a part of a <see cref="World"/>
    /// that can be simulated on its own.
    /// </summary>
    public class Chunk
    {
        private readonly List<Medium> mediums = new();
        private readonly List<Node> nodes = new();

        /// <summary>
        /// All <see cref="Medium"/>s that are at least
        /// partly contained inside this <see cref="Chunk"/>'s
        /// <see cref="Coordinates"/>.
        /// </summary>
        public IReadOnlyList<Medium> Mediums => mediums;

        /// <summary>
        /// All <see cref="Node"/>s with <see cref="Node.Position"/>
        /// inside this <see cref="Chunk"/>'s <see cref="Coordinates"/>.
        /// </summary>
        public IReadOnlyList<Node> Nodes => nodes;

        public Vector2 Index { get; }
        public RectangleF Coordinates { get; }

        public object SynchronizedTime { get; private set; }

        public Chunk(Vector2 index, float size)
        {
            Index = index;
            var coordinates = new RectangleF
            {
                Left = index.X * size,
                Bottom = index.Y * size
            };
            coordinates.Right = coordinates.Left + size;
            coordinates.Top = coordinates.Bottom + size;
            Coordinates = coordinates;
        }

        /// <summary>
        /// Simulates all <see cref="Nodes"/> in this <see cref="Chunk"/>.
        /// </summary>
        /// <returns>
        /// All <see cref="Node"/>s that go out of this <see cref="Chunk"/>'s
        /// boundaries in the process of simulation.
        /// </returns>
        public IEnumerable<Node> Simulate(object newTime)
        {
            foreach (var node in Nodes)
            {
                node.Simulate(new TimeSpan());
                if (!ElementaryCollisions.IsPointInRectangle(node.Position.ToTopDownPoint(), Coordinates))
                    yield return node;
            }
            SynchronizedTime = newTime;
        }

        public void AddMedium(Medium medium) => mediums.Add(medium);

        public void AddNode(Node node) => nodes.Add(node);

        public void RemoveMedium(Medium medium)
        {
            if (!mediums.Remove(medium))
                throw new InvalidOperationException($"The {nameof(medium)} wasn't in this chunk.");
        }

        public void RemoveNode(Node node)
        {
            if (!nodes.Remove(node))
                throw new InvalidOperationException($"The {nameof(node)} wasn't in this chunk.");
        }
    }
}