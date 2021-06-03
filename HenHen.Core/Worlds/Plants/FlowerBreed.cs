// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Random;
using System.Collections.Generic;

namespace HenHen.Core.Worlds.Plants
{
    public record FlowerBreed(string Name,
        IReadOnlyList<int> GrowthStagesDuration,
        IReadOnlyList<int> GrowthStagesDurationVariance,
        ChanceTable<int> DropAmountChanceTable,
        IReadOnlyList<string> PossibleSeasons) : PlantBreed(Name, GrowthStagesDuration, GrowthStagesDurationVariance);
}