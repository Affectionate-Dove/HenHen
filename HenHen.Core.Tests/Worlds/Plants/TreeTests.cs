// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Random;
using HenHen.Core.Worlds;
using HenHen.Core.Worlds.Plants;
using HenHen.Framework.Worlds;
using NUnit.Framework;
using System.Linq;

namespace HenHen.Core.Tests.Worlds.Plants
{
    public class TreeTests
    {
        private const int expected_drop_amount = 4;
        private TreeBreed treeBreed;
        private Tree tree;

        [SetUp]
        public void SetUp()
        {
            treeBreed = new TreeBreed("Some tree breed", new RandomHenHenTimeRange[]
            {
                new(HenHenTime.FromSeconds(2), HenHenTime.FromSeconds(2)),
                new(HenHenTime.FromSeconds(3), HenHenTime.FromSeconds(3)),
            }, expected_drop_amount, new(HenHenTime.FromSeconds(3), HenHenTime.FromSeconds(3)), new[] { "Spring" });

            tree = new Tree(treeBreed, new());
        }

        [Test]
        public void CollectableTest()
        {
            var worldTime = 0.1;
            tree.Simulate(worldTime);
            Assert.AreEqual(0, tree.GrowthStage);
            Assert.IsFalse(tree.Collectable);

            worldTime += tree.GrowthStagesDuration[0].Seconds;
            tree.Simulate(worldTime);
            Assert.AreEqual(1, tree.GrowthStage);
            Assert.IsFalse(tree.Collectable);

            worldTime += tree.GrowthStagesDuration[1].Seconds;
            tree.Simulate(worldTime);
            Assert.AreEqual(2, tree.GrowthStage);
            Assert.IsFalse(tree.Collectable);

            worldTime = tree.FruitsReadyDate.Seconds;
            tree.Simulate(worldTime);
            Assert.AreEqual(2, tree.GrowthStage);
            Assert.IsTrue(tree.Collectable);
        }

        [Test]
        public void DropAmountTest() => Assert.AreEqual(expected_drop_amount, tree.DropAmount);

        [Test]
        public void InteractionTest()
        {
            var world = new World();
            world.AddNode(tree);
            world.Simulate(10);

            Assert.IsTrue(tree.Collectable);
            Assert.NotNull(tree.Interaction);
            tree.Interaction();

            Assert.IsFalse(tree.Collectable);
            Assert.IsNull(tree.Interaction);

            world.Simulate(10);
            Assert.IsTrue(world.Nodes.Contains(tree));
            Assert.NotZero(tree.DropAmount);
            Assert.True(world.Nodes.Count(node => node is Fruit fruit && fruit.Name == tree.BreedName) == tree.DropAmount);
        }
    }
}