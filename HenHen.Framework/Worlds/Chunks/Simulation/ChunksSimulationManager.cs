// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;
using HenHen.Framework.Worlds.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace HenHen.Framework.Worlds.Chunks.Simulation
{
    /// <summary>
    ///     Manages organized <see cref="Chunk"/> simulation.
    /// </summary>
    public partial class ChunksSimulationManager
    {
        private readonly List<NodeTransfer> nodeTransfers = new();

        /// <summary>
        ///     The time at the start of the latest simulation.
        /// </summary>
        public double SynchronizedTime { get; private set; }

        protected ChunksManager ChunksManager { get; }

        public ChunksSimulationManager(ChunksManager chunksManager) => ChunksManager = chunksManager;

        /// <summary>
        ///     Simulates chunks according to <see cref="Strategy"/>.
        /// </summary>
        /// <param name="newTime">
        ///     The value to which <see cref="SynchronizedTime"/>
        ///     will be set at the beginning of this function.
        /// </param>
        /// <param name="origin">
        ///     The point around which the simulation occurs
        ///     according to <see cref="Strategy"/>.
        /// </param>
        public void Simulate(double newTime, Vector2 origin)
        {
            SynchronizedTime = newTime;
            foreach (var chunk in GetChunksToSimulate(origin))
                SimulateChunk(chunk, newTime);
            PerformNodesTransfers();
        }

        private IEnumerable<Chunk> GetChunksToSimulate(Vector2 origin) => throw new NotImplementedException();

        private void SimulateChunk(Chunk chunk, double newTime)
        {
            foreach (var simulatedNode in chunk.Simulate(newTime))
            {
                ContainNodeInMediums(simulatedNode);
                RegisterNodeTransfers(simulatedNode, chunk);
            }

            CollisionChecker.CheckNodeCollisions(chunk.Nodes, OnNodesCollision);
        }

        private void ContainNodeInMediums(Node node)
        {
            var mediumsToCheck = ChunksManager.GetChunksForNode(node).SelectMany(c => c.Mediums);
            if (!CollisionChecker.IsNodeContainedInMediums(node, mediumsToCheck))
                throw new NotImplementedException("Perform containing Node inside Mediums");
        }

        private void RegisterNodeTransfers(Node node, Chunk origin)
        {
            var stillInOrigin = false;
            foreach (var chunk in ChunksManager.GetChunksForNode(node))
            {
                if (chunk != origin)
                    nodeTransfers.Add(new NodeTransfer(node, NodeTransfer.TransferType.To, chunk));
                else
                    stillInOrigin = true;
            }
            if (!stillInOrigin)
                nodeTransfers.Add(new NodeTransfer(node, NodeTransfer.TransferType.From, origin));
        }

        private void PerformNodesTransfers() => nodeTransfers.ForEach(transfer => transfer.Perform());

        private void OnNodesCollision(Node a, Node b)
        {
            a.OnCollision(b);
            b.OnCollision(a);
        }
    }
}