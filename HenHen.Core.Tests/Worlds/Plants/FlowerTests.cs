// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Random;
using HenHen.Core.Worlds;
using HenHen.Core.Worlds.Plants;
using HenHen.Framework.Random;
using NUnit.Framework;

namespace HenHen.Core.Tests.Worlds.Plants
{
    public class FlowerTests
    {
        private FlowerBreed flowerBreed;
        private Flower flower;

        [SetUp]
        public void SetUp()
        {
            flowerBreed = new FlowerBreed("Some flower breed", new RandomHenHenTimeRange[]
            {
                new(HenHenTime.FromSeconds(2), HenHenTime.FromSeconds(2)),
                new(HenHenTime.FromSeconds(3), HenHenTime.FromSeconds(3))
            }, new(new ChanceTableEntry<int>[]
            {
                new(2, 7), new(3, 3)
            }),
            new[] { "Spring" });

            flower = new Flower(flowerBreed, new HenHenTime());
        }

        [Test]
        public void CollectableTest()
        {
            var worldTime = 0.1;
            flower.Simulate(worldTime);
            Assert.AreEqual(0, flower.GrowthStage);
            Assert.IsFalse(flower.Collectable);

            worldTime += flower.GrowthStagesDuration[0].Seconds;
            flower.Simulate(worldTime);
            Assert.AreEqual(1, flower.GrowthStage);
            Assert.IsFalse(flower.Collectable);

            worldTime += flower.GrowthStagesDuration[1].Seconds;
            flower.Simulate(worldTime);
            Assert.AreEqual(2, flower.GrowthStage);
            Assert.IsTrue(flower.Collectable);
        }

        [Test]
        public void DropAmountTest()
        {
            var dropAmount2Count = 0;
            var dropAmount3Count = 0;

            for (var i = 0; i < 1000; i++)
            {
                var flower = new Flower(flowerBreed, new HenHenTime());
                if (flower.DropAmount == 2)
                    dropAmount2Count++;
                else if (flower.DropAmount == 3)
                    dropAmount3Count++;
                else
                    Assert.Fail("The drop amount wasn't 2 nor 3.");
            }

            Assert.Greater(dropAmount2Count, 500);
            Assert.Less(dropAmount3Count, 500);
        }
    }
}