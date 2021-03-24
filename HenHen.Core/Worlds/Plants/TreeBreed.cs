// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;

namespace HenHen.Core.Worlds.Plants
{
    public record TreeBreed(string Name,
        IReadOnlyList<int> GrowthStagesDuration,
        IReadOnlyList<int> GrowthStagesDurationVariance,
        int MaxFruits,
        int FruitGrowthDuration,
        int FruitGrowthDurationVariance,
        IReadOnlyList<string> Seasons);
}