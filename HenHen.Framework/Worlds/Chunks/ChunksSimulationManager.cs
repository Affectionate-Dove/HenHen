// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using System;

namespace HenHen.Framework.Worlds.Chunks
{
    public class ChunksSimulationManager
    {
        public ChunksManager ChunksManager { get; }

        public ChunksSimulationManager(ChunksManager chunksManager) => ChunksManager = chunksManager;

        private void SimulateAllChunks(object newTime)
        {
            foreach (var chunk in ChunksManager.Chunks.Values)
                SimulateChunk(chunk, newTime);
        }

        private void SimulateChunksInRectangle(RectangleF rectangle, object newTime)
        {
            foreach (var chunk in ChunksManager.GetChunksForRectangle(rectangle))
                SimulateChunk(chunk, newTime);
        }

        private void SimulateChunksInCircle(Circle circle, object newTime)
        {
            foreach (var chunk in ChunksManager.GetChunksForCircle(circle))
                SimulateChunk(chunk, newTime);
        }

        private void SimulateChunk(Chunk chunk, object newTime) => throw new NotImplementedException();
    }

    public enum ChunksSimulationStrategy
    {
        All,
        Square,
        Radius
    }
}