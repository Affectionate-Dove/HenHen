// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Random;
using System.Collections.Generic;

namespace HenHen.Core.Worlds.Plants
{
    public class Tree : Plant
    {
        public int MaxFruits { get; }
        public RandomHenHenTimeRange FruitGrowthDuration { get; }
        public IReadOnlyList<string> Seasons { get; }

        public Tree(TreeBreed breed) : base(breed)
        {
            MaxFruits = breed.MaxFruits;
            FruitGrowthDuration = breed.FruitGrowthDuration;
            Seasons = breed.Seasons;
        }
    }
}