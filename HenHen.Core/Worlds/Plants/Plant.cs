// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Worlds.Nodes;
using System.Collections.Generic;

namespace HenHen.Core.Worlds.Plants
{
    public abstract class Plant : Node
    {
        public HenHenTime BirthDate { get; private init; }

        public string BreedName { get; protected init; }

        public IReadOnlyList<HenHenTime> GrowthStagesDuration { get; protected init; }

        public int GrowthStage
        {
            get
            {
                var currentStage = 0;
                var sum = new HenHenTime();
                var timeAlive = HenHenTime.FromSeconds(SynchronizedTime) - BirthDate;
                foreach (var stageDuration in GrowthStagesDuration)
                {
                    sum += stageDuration;

                    if (timeAlive <= sum)
                        return currentStage;

                    currentStage++;
                }
                return currentStage;
            }
        }

        public Plant(PlantBreed breed)
        {
            BreedName = breed.Name;
            var growthStagesDuration = new List<HenHenTime>();
            for (var i = 0; i < breed.GrowthStagesDurations.Count; i++)
            {
                growthStagesDuration[i] = breed.GrowthStagesDurations[i].GetRandom();
            }
            GrowthStagesDuration = growthStagesDuration;
        }
    }
}