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

        public ChunksManager ChunksManager { get; }

        public ChunksSimulationStrategy Strategy { get; set; }

        public ChunksSimulationManager(ChunksManager chunksManager) => ChunksManager = chunksManager;

        public void Simulate(double newTime, Vector2 origin = default)
        {
            var simulationRadius = GetRadiusToSimulateAndMarkAsSimulated(newTime);
            switch (Strategy.StrategyType)
            {
                case ChunksSimulationStrategyType.All:
                    SimulateAllChunks(newTime);
                    break;

                case ChunksSimulationStrategyType.Square:
                    var square = new RectangleF
                    {
                        Left = origin.X -= simulationRadius,
                        Right = origin.Y += simulationRadius,
                        Top = origin.Y += simulationRadius,
                        Bottom = origin.Y -= simulationRadius
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
            chunk.Simulate(newTime);
            Collisions.CollisionChecker.CheckNodeCollisions(chunk.Nodes, OnNodesCollision);
        }

        private void OnNodesCollision(Node a, Node b)
        {
            a.OnCollision(b);
            b.OnCollision(a);
        }
    }
}