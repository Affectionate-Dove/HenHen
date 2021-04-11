// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;

namespace HenHen.Framework.Worlds.Chunks
{
    public record ChunksSimulationStrategy
    {
        public IReadOnlyCollection<SimulationRingConfiguration> Rings { get; init; } = new List<SimulationRingConfiguration>();

        public ChunksSimulationStrategyType StrategyType { get; init; }
    }

    public enum ChunksSimulationStrategyType
    {
        All,
        Square,
        Radius
    }
}