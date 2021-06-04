// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Random;
using HenHen.Core.Worlds;
using HenHen.Core.Worlds.Plants;
using NUnit.Framework;
using System.Collections.Generic;

namespace HenHen.Core.Tests.Worlds.Plants
{
    public class PlantTests
    {
        private TestPlantBreed plantBreed;
        private TestPlant plant;

        [SetUp]
        public void SetUp()
        {
            plantBreed = new TestPlantBreed("Some plant", new RandomHenHenTimeRange[]
            {
                new(HenHenTime.FromSeconds(2), HenHenTime.FromSeconds(3)),
                new(HenHenTime.FromSeconds(7), HenHenTime.FromSeconds(12)),
                new(HenHenTime.FromSeconds(1), HenHenTime.FromSeconds(2))
            });

            plant = new TestPlant(plantBreed, HenHenTime.FromSeconds(0));
        }

        [Test]
        public void CtorTest()
        {
            Assert.GreaterOrEqual(plant.GrowthStagesDuration[0].Seconds, 2);
            Assert.LessOrEqual(plant.GrowthStagesDuration[0].Seconds, 3);

            Assert.GreaterOrEqual(plant.GrowthStagesDuration[1].Seconds, 7);
            Assert.LessOrEqual(plant.GrowthStagesDuration[1].Seconds, 12);

            Assert.GreaterOrEqual(plant.GrowthStagesDuration[2].Seconds, 1);
            Assert.LessOrEqual(plant.GrowthStagesDuration[2].Seconds, 2);
        }

        [Test]
        public void GrowthStageTest()
        {
            var worldTime = 0.1;
            plant.Simulate(worldTime);
            Assert.AreEqual(0, plant.GrowthStage);

            worldTime += plant.GrowthStagesDuration[0].Seconds;
            plant.Simulate(worldTime);
            Assert.AreEqual(1, plant.GrowthStage);

            worldTime += plant.GrowthStagesDuration[1].Seconds;
            plant.Simulate(worldTime);
            Assert.AreEqual(2, plant.GrowthStage);

            worldTime += plant.GrowthStagesDuration[2].Seconds;
            plant.Simulate(worldTime);
            Assert.AreEqual(3, plant.GrowthStage);

            worldTime += 100000;
            plant.Simulate(worldTime);
            Assert.AreEqual(3, plant.GrowthStage);
        }

        private record TestPlantBreed(string Name, IReadOnlyList<RandomHenHenTimeRange> GrowthStagesDurations) : PlantBreed(Name, GrowthStagesDurations);

        private class TestPlant : Plant
        {
            public TestPlant(TestPlantBreed breed, HenHenTime birthDate) : base(breed, birthDate)
            {
            }
        }
    }
}