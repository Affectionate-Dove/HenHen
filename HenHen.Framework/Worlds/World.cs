// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Worlds.Chunks;
using HenHen.Framework.Worlds.Chunks.Simulation;
using HenHen.Framework.Worlds.Mediums;
using HenHen.Framework.Worlds.Nodes;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Worlds
{
    public class World
    {
        private readonly List<Node> nodes = new();
        private readonly List<Medium> mediums = new();

        private readonly ChunksManager chunksManager;
        private readonly ChunksSimulationManager chunksSimulationManager;

        public IReadOnlyList<Node> Nodes => nodes;

        public float ChunkSize => chunksManager.ChunkSize;
        public double SynchronizedTime => chunksSimulationManager.SynchronizedTime;

        public World() : this(new Vector2(100))
        {
        }

        public World(Vector2 minimumSize) : this(minimumSize, 16)
        {
        }

        public World(Vector2 minimumSize, float chunkSize)
        {
            var chunkCount = new Vector2(MathF.Ceiling(minimumSize.X / chunkSize), MathF.Ceiling(minimumSize.Y / chunkSize));
            chunksManager = new(chunkCount, chunkSize);
            chunksSimulationManager = new ChunksSimulationManager(chunksManager);
        }

        public void AddNode(Node node)
        {
            nodes.Add(node);
            chunksManager.AddNode(node);
            node.NodeEjected += OnNodeEjected;
        }

        public void AddMedium(Medium medium)
        {
            mediums.Add(medium);
            chunksManager.AddMedium(medium);
        }

        public void Simulate(double newTime) => chunksSimulationManager.Simulate(newTime);

        private void OnNodeEjected(Node node) => AddNode(node);
    }
}