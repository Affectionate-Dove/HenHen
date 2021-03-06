﻿// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Random;
using System.Collections.Generic;

namespace HenHen.Core.Worlds.Plants
{
    public class Tree : Plant
    {
        public RandomHenHenTimeRange FruitsGrowthDuration { get; }
        public IReadOnlyList<string> Seasons { get; }
        public HenHenTime FruitsReadyDate { get; protected set; }

        public override bool Collectable => HenHenTime.FromSeconds(SynchronizedTime) >= FruitsReadyDate;

        public Tree(TreeBreed breed, HenHenTime birthDate) : base(breed, birthDate)
        {
            DropAmount = breed.FruitsAmount;
            FruitsGrowthDuration = breed.FruitsGrowthDuration;
            Seasons = breed.Seasons;
            FruitsReadyDate = GetInitialFruitsReadyDate(birthDate, GrowthStagesDuration, FruitsGrowthDuration);
        }

        protected override void AfterFruitsDrop()
        {
            base.AfterFruitsDrop();
            FruitsReadyDate = HenHenTime.FromSeconds(SynchronizedTime) + FruitsGrowthDuration.GetRandom();
        }

        private static HenHenTime GetInitialFruitsReadyDate(HenHenTime birthDate, IReadOnlyList<HenHenTime> growthStagesDuration, RandomHenHenTimeRange fruitsGrowthDuration)
        {
            var fruitsReadyDate = birthDate;

            foreach (var growthStageDuration in growthStagesDuration)
                fruitsReadyDate += growthStageDuration;

            fruitsReadyDate += fruitsGrowthDuration.GetRandom();

            return fruitsReadyDate;
        }
    }
}