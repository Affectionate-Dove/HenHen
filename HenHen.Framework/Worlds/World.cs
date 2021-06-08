// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using HenHen.Framework.Worlds.Chunks;
using HenHen.Framework.Worlds.Chunks.Simulation;
using HenHen.Framework.Worlds.Mediums;
using HenHen.Framework.Worlds.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace HenHen.Framework.Worlds
{
    public class World
    {
        private readonly ChunksManager chunksManager;
        private readonly ChunksSimulationManager chunksSimulationManager;

        public IEnumerable<Node> Nodes => chunksManager.Chunks.Values.SelectMany(chunk => chunk.Nodes).Distinct();

        public double SynchronizedTime => chunksSimulationManager.SynchronizedTime;

        public Vector2 Size => chunksManager.ChunkCount * chunksManager.ChunkSize;

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
            chunksManager.AddNode(node);
            node.NodeEjected += OnNodeEjected;
        }

        public void AddMedium(Medium medium) => chunksManager.AddMedium(medium);

        public void Simulate(double newTime) => chunksSimulationManager.Simulate(newTime);

        /// <summary>
        ///     Returns <see cref="Medium"/> mediums that
        ///     are in chunks that are inside
        ///     the specified <paramref name="area"/>.
        /// </summary>
        public IEnumerable<Medium> GetMediumsAroundArea(RectangleF area) => chunksManager.GetChunksForRectangle(area).SelectMany(chunk => chunk.Mediums);

        private void OnNodeEjected(Node node) => AddNode(node);
    }
}