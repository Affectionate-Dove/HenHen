// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Random;
using HenHen.Core.Worlds;
using HenHen.Core.Worlds.Plants;
using HenHen.Framework.Extensions;
using HenHen.Framework.Graphics2d;
using HenHen.Framework.Random;
using HenHen.Framework.UI;
using HenHen.Framework.VisualTests;
using HenHen.Framework.Worlds.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace HenHen.Core.VisualTests.Worlds
{
    public class PlantsTestScene : VisualTestScene
    {
        private readonly FlowerBreed flowerBreed;
        private readonly TreeBreed treeBreed;
        private readonly Flower[] flowers;
        private readonly Tree[] trees;
        private readonly List<Node> nodes = new();
        private readonly SpriteText timeText;

        public PlantsTestScene()
        {
            flowerBreed = CreateFlowerBreed();
            treeBreed = CreateTreeBreed();

            var visualPlantsContainer = new Container
            {
                Anchor = new(0.5f),
                Origin = new(0.5f),
                Size = new(600, 300)
            };
            AddChild(visualPlantsContainer);

            const int trees_amount = 30;
            trees = new Tree[trees_amount];
            for (var i = 0; i < trees_amount; i++)
            {
                trees[i] = new(treeBreed, HenHenTime.FromSeconds(0));
                nodes.Add(trees[i]);
                visualPlantsContainer.AddChild(new TreeDisplay(trees[i])
                {
                    Size = new(40, 120),
                    Origin = new(0.5f, 1),
                    RelativePositionAxes = Axes.Both,
                    Offset = new((float)RNG.GetDouble(), (float)RNG.GetDouble())
                });
            }

            const int flowers_amount = 70;
            flowers = new Flower[flowers_amount];
            for (var i = 0; i < flowers_amount; i++)
            {
                flowers[i] = new(flowerBreed, HenHenTime.FromSeconds(0));
                nodes.Add(flowers[i]);
                visualPlantsContainer.AddChild(new FlowerDisplay(flowers[i], flowerBreed)
                {
                    Size = new(20, 40),
                    Origin = new(0.5f, 1),
                    RelativePositionAxes = Axes.Both,
                    Offset = new((float)RNG.GetDouble(), (float)RNG.GetDouble())
                });
            }
            SortChildrenByDistance(visualPlantsContainer);

            //var flowerInfo = "Flower growth stages durations:\n";
            //for (var i = 0; i < flowers.GrowthStagesDuration.Count; i++)
            //    flowerInfo += $"Stage {i + 1}: {flowers.GrowthStagesDuration[i].Seconds:0}s\n";
            //AddChild(new SpriteText
            //{
            //    Size = new(300, 100),
            //    Offset = new(-200, 220),
            //    Anchor = new(0.5f),
            //    Origin = new(0.5f, 0),
            //    Text = flowerInfo
            //});

            AddChild(timeText = new SpriteText
            {
                Size = new(300, 50),
                Offset = new(0, 200),
                Anchor = new(0.5f),
                Origin = new(0.5f, 0),
                AlignMiddle = true
            });
        }

        protected override void OnUpdate(float elapsed)
        {
            var newTime = nodes[0].SynchronizedTime + 0.005f;

            foreach (var node in nodes)
                node.Simulate(newTime);

            timeText.Text = newTime.ToString("0");
            base.OnUpdate(elapsed);

            if (nodes.All(node => node.Interaction is not null))
                InteractAll();
        }

        private static void SortChildrenByDistance(Container container) => container.Children.Sort((a, b) => Math.Sign(a.Offset.Y - b.Offset.Y));

        private static FlowerBreed CreateFlowerBreed() => new
        (
            Name: "Daisy",
            GrowthStagesDurations: new RandomHenHenTimeRange[]
            {
                new(HenHenTime.FromSeconds(2), HenHenTime.FromSeconds(6)),
                new(HenHenTime.FromSeconds(5), HenHenTime.FromSeconds(10))
            },
            DropAmount: new ChanceTable<int>(new ChanceTableEntry<int>[]
            {
                new(2, 5),
                new(3, 2),
                new(4, 1)
            }),
            PossibleSeasons: new[] { "Spring" }
        );

        private static TreeBreed CreateTreeBreed() => new
        (
            Name: "Apple",
            GrowthStagesDurations: new RandomHenHenTimeRange[]
            {
                new(HenHenTime.FromSeconds(2), HenHenTime.FromSeconds(6)),
                new(HenHenTime.FromSeconds(5), HenHenTime.FromSeconds(10))
            },
            FruitsAmount: 3,
            FruitsGrowthDuration: new RandomHenHenTimeRange(HenHenTime.FromSeconds(2), HenHenTime.FromSeconds(10)),
            Seasons: new[] { "Spring" }
        );

        private void InteractAll()
        {
            foreach (var node in nodes)
                node.Interaction();
        }

        private class FlowerDisplay : Container
        {
            private readonly Circle fruit;
            private readonly Container fruitContainer;
            private readonly Rectangle stem;

            public Flower Flower { get; }

            public FlowerBreed Breed { get; }

            public FlowerDisplay(Flower flower, FlowerBreed breed)
            {
                Flower = flower;
                Breed = breed;
                AddChild(stem = new Rectangle
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new(0.2f, 0),
                    Color = new(0, 100, 0),
                    Anchor = new(0.5f, 1),
                    Origin = new(0.5f, 1)
                });
                AddChild(fruitContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = new(0.5f, 0),
                    Origin = new(0.5f),
                });
                fruitContainer.AddChild(fruit = new Circle
                {
                    RelativeSizeAxes = Axes.Both,
                    Origin = new(0.5f),
                    Anchor = new(0.5f)
                });
                const int petals_amount = 10;
                const float angle_difference = 360f / petals_amount;
                for (var i = 0; i < petals_amount; i++)
                {
                    var angle = angle_difference * i;
                    var offset_vector = new Vector2(0, 0.5f).GetRotated(angle);
                    fruitContainer.AddChild(new Circle
                    {
                        RelativeSizeAxes = Axes.Both,
                        Size = new(0.38f),
                        Origin = new(0.5f),
                        Anchor = new(0.5f),
                        Offset = offset_vector,
                        RelativePositionAxes = Axes.Both
                    });
                }
            }

            protected override void OnUpdate(float elapsed)
            {
                var stemHeight = 0.2f + (0.8f * Flower.GrowthStage / Breed.GrowthStagesDurations.Count);
                stem.Size = new Vector2(0.2f, stemHeight);
                fruit.Color = Flower.Collectable ? new(255, 180, 0) : new(0, 100, 0);
                fruitContainer.Size = Flower.Collectable ? new(1, Size.X / Size.Y) : Vector2.Zero;
                base.OnUpdate(elapsed);
            }
        }

        private class TreeDisplay : Container
        {
            private readonly Rectangle trunk;
            private readonly Circle leaves;
            private readonly Tree tree;
            private readonly Container fruitsContainer;

            public TreeDisplay(Tree tree)
            {
                this.tree = tree;
                var brightness = RNG.GetDouble(0.5, 1);

                AddChild(trunk = new Rectangle
                {
                    Color = new ColorInfo(70, 50, 20),
                    Anchor = new(0.5f, 1),
                    Origin = new(0.5f, 1),
                    RelativeSizeAxes = Axes.Both
                });
                AddChild(leaves = new Circle
                {
                    Color = new ColorInfo(0, (byte)(180 * brightness), 0),
                    Anchor = new(0.5f, 1),
                    Origin = new(0.5f),
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both
                });
                AddChild(fruitsContainer = new Container
                {
                    Anchor = new(0.5f, 1),
                    Origin = new(0.5f),
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both
                });

                var angle_difference = 360f / tree.DropAmount;
                for (var i = 0; i < tree.DropAmount; i++)
                {
                    var angle = 42 + (angle_difference * i);
                    var offset_vector = new Vector2(0, 0.3f).GetRotated(angle);
                    fruitsContainer.AddChild(new Circle
                    {
                        Origin = new(0.5f),
                        Anchor = new(0.5f),
                        Offset = offset_vector,
                        RelativePositionAxes = Axes.Both,
                        RelativeSizeAxes = Axes.Both
                    });
                }
            }

            protected override void OnUpdate(float elapsed)
            {
                base.OnUpdate(elapsed);
                trunk.Size = CalculateTrunkSize();
                var leavesWidth = CalculateLeavesRadius() * 2;
                fruitsContainer.Size = leaves.Size = new(leavesWidth, leavesWidth * (Size.X / Size.Y));
                fruitsContainer.Offset = leaves.Offset = new(0, -trunk.Size.Y);
                var treeReady = tree.GrowthStage == tree.GrowthStagesDuration.Count;
                var fruitsReady = tree.Collectable;
                foreach (var fruit in fruitsContainer.Children.OfType<Circle>())
                {
                    fruit.Color = new((byte)(fruitsReady ? 255 : 50), (byte)(fruitsReady ? 0 : 60), 0, (byte)(treeReady ? 255 : 0));
                    if (treeReady)
                        fruit.Size = new(GetFruitSize());
                }
            }

            private float GetFruitSize()
            {
                const float max_size = 0.1f;
                var growthStart = tree.FruitsReadyDate - tree.FruitsGrowthDuration.Max;
                var growingFor = HenHenTime.FromSeconds(tree.SynchronizedTime) - growthStart;
                if (growingFor > tree.FruitsGrowthDuration.Max)
                    return max_size;
                return max_size * (float)(growingFor / tree.FruitsGrowthDuration.Max);
            }

            private Vector2 CalculateTrunkSize()
            {
                const float max_height = 1f;
                const float min_height = 1 / 8f;
                var height = min_height + (max_height * tree.GrowthStage / (tree.GrowthStagesDuration.Count + 1f));
                var width = tree.GrowthStage == tree.GrowthStagesDuration.Count ? 0.3f : 0.2f;
                return new(width, height);
            }

            private float CalculateLeavesRadius()
            {
                const float max_radius = 1;
                return max_radius * tree.GrowthStage / tree.GrowthStagesDuration.Count;
            }
        }
    }
}