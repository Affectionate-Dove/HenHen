// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;

namespace HenHen.Core.Worlds.Plants
{
    public record FlowerBreed(string Name,
        IReadOnlyList<int> GrowthStagesDuration,
        IReadOnlyList<int> GrowthStagesDurationVariance,
        IReadOnlyList<(int amount, int chance)> DropAmount,
        IReadOnlyList<string> PossibleSeasons);
}