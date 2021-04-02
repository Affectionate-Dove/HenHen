// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Extensions;
using HenHen.Framework.Numerics;
using HenHen.Framework.Worlds.Mediums;
using HenHen.Framework.Worlds.Nodes;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Worlds.Chunks
{
    public class Chunk
    {
        private readonly List<Medium> mediums = new();
        private readonly List<Node> nodes = new();

        public Vector2 Index { get; }
        public ChunksManager Manager { get; }
        public IReadOnlyCollection<Medium> Mediums => mediums;
        public IReadOnlyCollection<Node> Nodes => nodes;
        public object SynchronizedTime { get; private set; }
        public RectangleF Coordinates { get; }

        public Chunk(Vector2 index, ChunksManager manager)
        {
            Index = index;
            Manager = manager;
            var coordinates = new RectangleF
            {
                Left = index.X * manager.ChunkSize,
                Bottom = index.Y * manager.ChunkSize
            };
            coordinates.Right = coordinates.Left + manager.ChunkSize;
            coordinates.Top = coordinates.Bottom + manager.ChunkSize;
            Coordinates = coordinates;
        }

        public IEnumerable<ChunkTransfer> SimulateChunk(object newTime)
        {
            foreach (var node in Nodes)
            {
                node.Simulate(newTime);
                var targetChunk = Manager.GetChunkForPosition(node.Position.ToTopDownPoint());
                yield return new ChunkTransfer { Node = node, From = this, To = targetChunk };
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