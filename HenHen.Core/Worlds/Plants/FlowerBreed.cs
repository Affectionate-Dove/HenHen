// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Random;
using HenFwork.Random;
using System.Collections.Generic;

namespace HenHen.Core.Worlds.Plants
{
    public record FlowerBreed(string Name,
        IReadOnlyList<RandomHenHenTimeRange> GrowthStagesDurations,
        ChanceTable<int> DropAmount,
        IReadOnlyList<string> PossibleSeasons) : PlantBreed(Name, GrowthStagesDurations);
}