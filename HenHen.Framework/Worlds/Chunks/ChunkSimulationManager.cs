// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;
using HenHen.Framework.Numerics;
using HenHen.Framework.Worlds.Nodes;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Worlds.Chunks
{
    public class ChunkSimulationManager
    {
        private readonly ICollection<ChunkTransfer> nodeTransfers = new List<ChunkTransfer>();
        private readonly List<Chunk> simulationQueue = new();
        private int maxSimulationStepTimeSpan = 1;
        public ChunksManager ChunksManager { get; }

        /// <summary>
        /// When simulating a chunk that wasn't simulated
        /// for a long duration, if the duration exceeds
        /// this value, it gets subdivided into more steps.
        /// If set to 0, this functionality is disabled.
        /// </summary>
        public int MaxSimulationStepTimeSpan
        {
            get => maxSimulationStepTimeSpan;
            set
            {
                if (value < 0)
                    throw new Exception("Cannot be less than 0.");
                maxSimulationStepTimeSpan = value;
            }
        }

        public ChunkSimulationManager(ChunksManager chunkManager) => ChunksManager = chunkManager;

        public void SimulateAllChunks(object newTime)
        {
            simulationQueue.AddRange(ChunksManager.Chunks.Values);
            SimulateQueriedChunks(newTime);
            PerformTransfers();
        }

        /// <summary>
        /// Simulates all <see cref="Chunks"/> that have their center
        /// in the specified <paramref name="circle"/>.
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="newTime"></param>
        public void SimulateChunksInCircle(Circle circle, object newTime)
        {
            var bottomLeft = circle.CenterPosition - new Vector2(circle.Radius);
            var topRight = circle.CenterPosition + new Vector2(circle.Radius);
            var rect = new RectangleF { Left = bottomLeft.X, Right = topRight.X, Top = topRight.Y, Bottom = bottomLeft.Y };
            foreach (var chunk in ChunksManager.GetChunks(rect))
            {
                if (ElementaryCollisions.IsPointInCircle(chunk.Coordinates.Center, circle))
                    SimulateChunk(chunk, newTime);
            }
        }

        protected void PerformTransfers()
        {
            foreach (var transfer in nodeTransfers)
            {
                transfer.From.RemoveNode(transfer.Node);
                transfer.To.AddNode(transfer.Node);
            }
            nodeTransfers.Clear();
        }

        protected void SimulateChunk(Chunk chunk, object newTime)
        {
            // TODO: implement MaxSimulationStepTimeSpan
            CollisionChecker.CheckNodeCollisions(chunk.Nodes, OnNodesCollision);
            foreach (var nodeTransfer in chunk.SimulateChunk(newTime))
                nodeTransfers.Add(nodeTransfer);
        }

        private void SimulateQueriedChunks(object newTime)
        {
            foreach (var chunk in simulationQueue)
                SimulateChunk(chunk, newTime);
            simulationQueue.Clear();
            PerformTransfers();
        }

        private void OnNodesCollision(Node a, Node b)
        {
            a.OnCollision(b);
            b.OnCollision(a);
        }
    }
}