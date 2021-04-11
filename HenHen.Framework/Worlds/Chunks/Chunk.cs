// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

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
        private readonly List<Medium> mediumsList = new();

        // Nodes need to be stored both in a list and in a hashset.
        // Publicly exposing them in a list is needed for
        // collision checking functions, to avoid casting hashset to list.
        // HashSet is needed to avoid having to check whether a node
        // is already present using List.Contains, which is slow,
        // and it also avoids having to store the presence of each node
        // elsewhere.
        private readonly List<Node> nodesList = new();

        private readonly HashSet<Node> nodesHashSet = new();

        /// <summary>
        /// All <see cref="Medium"/>s that are at least
        /// partly contained inside this <see cref="Chunk"/>'s
        /// <see cref="Coordinates"/>.
        /// </summary>
        public IReadOnlyList<Medium> Mediums => mediumsList;

        /// <summary>
        /// All <see cref="Node"/>s with <see cref="Node.Position"/>
        /// inside this <see cref="Chunk"/>'s <see cref="Coordinates"/>.
        /// </summary>
        public IReadOnlyList<Node> Nodes => nodesList;

        public Vector2 Index { get; }
        public RectangleF Coordinates { get; }

        public double SynchronizedTime { get; private set; }

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
        /// All <see cref="Node"/>s that aren't fully contained inside
        /// this <see cref="Chunk"/>'s boundaries
        /// after the process of simulation.
        /// </returns>
        public IEnumerable<Node> Simulate(double newTime)
        {
            if (newTime < SynchronizedTime)
                throw new ArgumentOutOfRangeException(nameof(newTime), $"New time has to be greater or equal to {nameof(SynchronizedTime)}");

            foreach (var node in Nodes)
            {
                node.Simulate(newTime);
                if (!IsRectFullyInside((node.CollisionBody.BoundingBox + node.Position).ToTopDownRectangle()))
                    yield return node;
            }
            SynchronizedTime = newTime;
        }

        public void AddMedium(Medium medium) => mediumsList.Add(medium);

        public void AddNode(Node node)
        {
            if (nodesHashSet.Add(node))
                nodesList.Add(node);
            // only add node to list if it isn't already in this chunk
        }

        public void RemoveMedium(Medium medium)
        {
            if (!mediumsList.Remove(medium))
                throw new InvalidOperationException($"The {nameof(medium)} wasn't in this chunk.");
        }

        public void RemoveNode(Node node)
        {
            if (!nodesList.Remove(node))
                throw new InvalidOperationException($"The {nameof(node)} wasn't in this chunk.");
            if (!nodesHashSet.Remove(node))
                throw new InvalidOperationException($"The {nameof(node)} was in {nameof(nodesList)} but wasn't in {nameof(nodesHashSet)}.");
        }

        private bool IsRectFullyInside(RectangleF rect) => rect.Left > Coordinates.Left
            && rect.Right < Coordinates.Right
            && rect.Top < Coordinates.Top
            && rect.Bottom > Coordinates.Bottom;
    }
}