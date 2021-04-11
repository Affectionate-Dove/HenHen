// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using HenHen.Framework.Worlds.Nodes;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Worlds.Chunks
{
    public class ChunksSimulationManager
    {
        private readonly SortedSet<SimulationRingInfo> simulationRings = new();
        private readonly List<NodeTransfer> transfers = new();
        private ChunksSimulationStrategy strategy;

        public ChunksSimulationStrategy Strategy
        {
            get => strategy;
            set
            {
                strategy = value;
                simulationRings.Clear();
                foreach (var ringConfiguration in value.Rings)
                    simulationRings.Add(new SimulationRingInfo(ringConfiguration));
            }
        }

        protected ChunksManager ChunksManager { get; }

        public ChunksSimulationManager(ChunksManager chunksManager) => ChunksManager = chunksManager;

        public void Simulate(double newTime, Vector2 origin)
        {
            var simulationRadius = GetRadiusToSimulateAndMarkAsSimulated(newTime);
            if (float.IsPositiveInfinity(simulationRadius))
                SimulateAllChunks(newTime);
            else
            {
                switch (Strategy.StrategyType)
                {
                    case ChunksSimulationStrategyType.All:
                        SimulateAllChunks(newTime);
                        break;

                    case ChunksSimulationStrategyType.Square:
                        var square = new RectangleF
                        {
                            Left = origin.X - simulationRadius,
                            Right = origin.Y + simulationRadius,
                            Top = origin.Y + simulationRadius,
                            Bottom = origin.Y - simulationRadius
                        };
                        SimulateChunksInRectangle(square, newTime);
                        break;

                    case ChunksSimulationStrategyType.Radius:
                        var circle = new Circle
                        {
                            CenterPosition = origin,
                            Radius = simulationRadius
                        };
                        SimulateChunksInCircle(circle, newTime);
                        break;

                    default:
                        break;
                }
            }
            PerformTransfers();
        }

        private float GetRadiusToSimulateAndMarkAsSimulated(double newTime)
        {
            foreach (var ring in simulationRings)
            {
                if (ring.ShouldBeSimulated(newTime))
                    ring.OnSimulation(newTime);
                else
                    return ring.InnerBoundary;
            }
            return float.PositiveInfinity;
        }

        private void SimulateAllChunks(double newTime)
        {
            foreach (var chunk in ChunksManager.Chunks.Values)
                SimulateChunk(chunk, newTime);
        }

        private void SimulateChunksInRectangle(RectangleF rectangle, double newTime)
        {
            foreach (var chunk in ChunksManager.GetChunksForRectangle(rectangle))
                SimulateChunk(chunk, newTime);
        }

        private void SimulateChunksInCircle(Circle circle, double newTime)
        {
            foreach (var chunk in ChunksManager.GetChunksForCircle(circle))
                SimulateChunk(chunk, newTime);
        }

        private void SimulateChunk(Chunk chunk, double newTime)
        {
            foreach (var node in chunk.Simulate(newTime))
            {
                var stillInThisChunk = false;
                foreach (var targetChunk in ChunksManager.GetChunksForNode(node))
                {
                    if (targetChunk == chunk)
                        stillInThisChunk = true;
                    else
                        transfers.Add(new NodeTransfer(node, targetChunk, NodeTransfer.TransferType.To));
                }

                if (!stillInThisChunk)
                    transfers.Add(new NodeTransfer(node, chunk, NodeTransfer.TransferType.From));
            }
            Collisions.CollisionChecker.CheckNodeCollisions(chunk.Nodes, OnNodesCollision);
        }

        private void PerformTransfers()
        {
            foreach (var transfer in transfers)
            {
                if (transfer.Type == NodeTransfer.TransferType.From)
                    transfer.Chunk.RemoveNode(transfer.Node);
                else
                    transfer.Chunk.AddNode(transfer.Node);
            }
        }

        private void OnNodesCollision(Node a, Node b)
        {
            a.OnCollision(b);
            b.OnCollision(a);
        }

        private readonly struct NodeTransfer
        {
            public Node Node { get; }
            public TransferType Type { get; }
            public Chunk Chunk { get; }

            public NodeTransfer(Node node, Chunk target, TransferType type)
            {
                Node = node;
                Chunk = target;
                Type = type;
            }

            public enum TransferType
            {
                From, To
            }
        }
    }
}