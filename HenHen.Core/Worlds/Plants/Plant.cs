// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Worlds.Nodes;
using System;
using System.Collections.Generic;

namespace HenHen.Core.Worlds.Plants
{
    public abstract class Plant : Node
    {
        public int TimeAlive { get; private set; }

        public string BreedName { get; protected init; }

        public IReadOnlyList<int> GrowthStagesDuration { get; protected init; }

        public int GrowthStage
        {
            get
            {
                var currentStage = 0;
                var sum = 0;
                foreach (var stageDuration in GrowthStagesDuration)
                {
                    sum += stageDuration;
                    if (TimeAlive <= sum)
                    {
                        return currentStage;
                    }
                    currentStage++;
                }
                return currentStage;
            }
        }

        public Plant(PlantBreed breed)
        {
            BreedName = breed.Name;
            var growthStagesDuration = new List<int>();
            var random = new Random();
            for (var i = 0; i < breed.GrowthStagesDuration.Count; i++)
            {
                growthStagesDuration[i] = (int)(breed.GrowthStagesDuration[i] + (random.NextDouble() * breed.GrowthStagesDurationVariance[i]));
            }
            GrowthStagesDuration = growthStagesDuration;
        }

        protected override void Simulation(double duration)
        {
            base.Simulation(duration);
            TimeAlive++;
        }
    }
}