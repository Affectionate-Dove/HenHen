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
using System.Numerics;

namespace HenHen.Core.VisualTests.Worlds
{
    public class PlantsTestScene : VisualTestScene
    {
        private readonly FlowerBreed flowerBreed;
        private readonly TreeBreed treeBreed;
        private readonly Flower flower;
        private readonly Tree tree;
        private readonly SpriteText timeText;

        public PlantsTestScene()
        {
            flowerBreed = new FlowerBreed(
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
                PossibleSeasons: new[] { "Spring" });

            treeBreed = new TreeBreed(
                Name: "Apple",
                GrowthStagesDurations: new RandomHenHenTimeRange[]
                {
                    new(HenHenTime.FromSeconds(2), HenHenTime.FromSeconds(6)),
                    new(HenHenTime.FromSeconds(5), HenHenTime.FromSeconds(10))
                },
                FruitsAmount: 3,
                FruitsGrowthDuration: new RandomHenHenTimeRange(HenHenTime.FromSeconds(2), HenHenTime.FromSeconds(10)),
                Seasons: new[] { "Spring" });

            flower = new Flower(flowerBreed, HenHenTime.FromSeconds(0));
            tree = new Tree(treeBreed, HenHenTime.FromSeconds(0));

            AddChild(new FlowerDisplay(flower, flowerBreed)
            {
                Offset = new(-200, 200),
                Anchor = new(0.5f),
                Origin = new(0.5f, 1)
            });

            var flowerInfo = "Flower growth stages durations:\n";
            for (var i = 0; i < flower.GrowthStagesDuration.Count; i++)
                flowerInfo += $"Stage {i + 1}: {flower.GrowthStagesDuration[i].Seconds:0}s\n";
            AddChild(new SpriteText
            {
                Size = new(300, 100),
                Offset = new(-200, 220),
                Anchor = new(0.5f),
                Origin = new(0.5f, 0),
                Text = flowerInfo
            });

            AddChild(timeText = new SpriteText
            {
                Size = new(300, 50),
                Offset = new(0, 200),
                Anchor = new(0.5f),
                Origin = new(0.5f, 0),
                AlignMiddle = true
            });
        }

        protected override void PreUpdate()
        {
            flower.Simulate(flower.SynchronizedTime + 0.005f);
            timeText.Text = flower.SynchronizedTime.ToString("0");
            base.PreUpdate();
        }

        private class FlowerDisplay : Container
        {
            private readonly Circle fruit;
            private readonly Container fruitContainer;

            public Flower Flower { get; }

            public FlowerBreed Breed { get; }

            public FlowerDisplay(Flower flower, FlowerBreed breed)
            {
                Flower = flower;
                Breed = breed;
                AddChild(new Rectangle // stem
                {
                    RelativeSizeAxes = Axes.Y,
                    Size = new(20, 1),
                    Color = new(0, 100, 0)
                });
                AddChild(fruitContainer = new Container
                {
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
                        Size = new(38),
                        Origin = new(0.5f),
                        Anchor = new(0.5f),
                        Offset = offset_vector,
                        RelativePositionAxes = Axes.Both
                    });
                }
            }

            protected override void PreUpdate()
            {
                var height = 50 + (200 * Flower.GrowthStage / (float)Breed.GrowthStagesDurations.Count);
                Size = new Vector2(20, height);
                fruit.Color = Flower.Collectable ? new(255, 180, 0) : new(0, 100, 0);
                fruitContainer.Size = new(Flower.Collectable ? 100 : 0);
                base.PreUpdate();
            }
        }
    }
}