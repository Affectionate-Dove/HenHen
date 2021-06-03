// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Random;
using System.Collections.Generic;

namespace HenHen.Core.Worlds.Plants
{
    public record TreeBreed(string Name,
        IReadOnlyList<RandomHenHenTimeRange> GrowthStagesDurations,
        int MaxFruits,
        RandomHenHenTimeRange FruitGrowthDuration,
        IReadOnlyList<string> Seasons) : PlantBreed(Name, GrowthStagesDurations);
}