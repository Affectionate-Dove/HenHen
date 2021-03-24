// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;
using System.Collections.Generic;

namespace HenHen.Core.Worlds.Plants
{
    public class Flower : Plant
    {
        public string BreedName;
        public IReadOnlyList<int> GrowthStagesDuration;
        public int DropAmount;
        public int GrowStage;
        public bool Collectable;

        public Flower(FlowerBreed breed)
        {
            BreedName = breed.Name;
            var growthStagesDuration = new List<int>();
            var random = new Random();
            for (int i = 0; i < breed.GrowthStagesDuration.Count; i++)
            {
                growthStagesDuration[i] = (int)(breed.GrowthStagesDuration[i] + (random.NextDouble() * breed.GrowthStagesDurationVariance[i]));
            }
            GrowthStagesDuration = growthStagesDuration;
            var randomPoint = random.NextDouble();
            var currentEndPoint = 0.0;
            var suma = 0.0;
            for (int i = 0; i < breed.DropAmount.Count; i++)
            {
                suma = suma + breed.DropAmount[i].chance;
            }
            for (int i = 0; i < breed.DropAmount.Count; i++)
            {
                currentEndPoint = currentEndPoint + (breed.DropAmount[i].chance / suma);
                if (randomPoint <= currentEndPoint)
                {
                    DropAmount = breed.DropAmount[i].amount;
                    break;
                }
            }
        }
    }
}