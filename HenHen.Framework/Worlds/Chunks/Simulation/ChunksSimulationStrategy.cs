// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;

namespace HenHen.Framework.Worlds.Chunks.Simulation
{
    public record ChunksSimulationStrategy
    {
        public ChunksSimulationStrategyType Type { get; }

        /// <summary>
        ///     Sorted set, with the smallest
        ///     <see cref="ChunksSimulationRing"/> first.
        /// </summary>
        public IReadOnlySet<ChunksSimulationRing> Rings { get; } = new SortedSet<ChunksSimulationRing>();

        public ChunksSimulationStrategy(ChunksSimulationStrategyType type, IReadOnlySet<ChunksSimulationRing> rings)
        {
            Type = type;
            Rings = rings;
        }
    }
}