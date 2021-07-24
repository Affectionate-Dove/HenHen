// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d.Worlds;
using HenHen.Framework.Numerics;
using HenHen.Framework.UI;
using HenHen.Framework.VisualTests.Input;
using HenHen.Framework.Worlds;
using HenHen.Framework.Worlds.Mediums;
using HenHen.Framework.Worlds.Nodes;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.VisualTests.Graphics2d.Worlds
{
    public class WorldViewer2dTestScene : VisualTestScene
    {
        private readonly WorldViewer2d worldViewer2d;
        private readonly World world;

        public WorldViewer2dTestScene()
        {
            world = new World(new Vector2(10), 2);
            AddNodes();
            foreach (var medium in GetSampleMediums())
            {
                world.AddMedium(medium);
            }
            worldViewer2d = new WorldViewer2d(world)
            {
                Size = new Vector2(200),
                Anchor = new Vector2(0.5f),
                Origin = new Vector2(0.5f)
            };
            AddChild(worldViewer2d);
            AddChild(GenerateInfoText());
        }

        public override bool OnActionPressed(SceneControls action)
        {
            switch (action)
            {
                case SceneControls.One:
                    worldViewer2d.GridDistance -= 0.5f;
                    return true;

                case SceneControls.Two:
                    worldViewer2d.GridDistance += 0.5f;
                    return true;

                case SceneControls.Up:
                    worldViewer2d.Target += new Vector2(0, 1);
                    return true;

                case SceneControls.Down:
                    worldViewer2d.Target -= new Vector2(0, 1);
                    return true;

                case SceneControls.Left:
                    worldViewer2d.Target -= new Vector2(1, 0);
                    return true;

                case SceneControls.Right:
                    worldViewer2d.Target += new Vector2(1, 0);
                    return true;

                case SceneControls.Nine:
                    worldViewer2d.FieldOfView -= 1;
                    return true;

                case SceneControls.Zero:
                    worldViewer2d.FieldOfView += 1;
                    return true;

                default:
                    return base.OnActionPressed(action);
            }
        }

        private static IEnumerable<Medium> GetSampleMediums() => new Medium[]
        {
            new Medium
            {
                Triangle = new Triangle3(new(-5, 0, 5), new(0, 0, -5), new(0, 0, 5)),
                Type = MediumType.Water
            },
            new Medium
            {
                Triangle = new Triangle3(new(-5, 0, 5), new(-5, 0, -5), new(0, 0, -5)
),
                Type = MediumType.Water
            },
            new Medium
            {
                Triangle = new Triangle3(new(0, 0, -5), new(2, 0, -5), new(2, 0, 5)),
                Type = MediumType.Ground
            },
            new Medium
            {
                Triangle = new Triangle3(new(0, 0, -5), new(2, 0, 5), new(0, 0, 5)),
                Type = MediumType.Ground
            },
        };

        private static SpriteText GenerateInfoText()
        {
            var t =
                "1 - decrease grid spacing by 0.5\n" +
                "2 - increase grid spacing by 0.5\n" +
                "↑/↓/←/→ - move view\n" +
                "9/0 - increase/decrease zoom";

            return new()
            {
                Anchor = new(1, 1),
                Origin = new(1, 1),
                Size = new(300, 600),
                Text = t
            };
        }

        private void AddNodes()
        {
            world.AddNode(new TestNode(new(1, 0, 1)));
            world.AddNode(new TestNode(new(0, 4, 5)));
            world.AddNode(new TestNode(new(4, 2, 0)));
        }

        private class TestNode : Node
        {
            public TestNode(Vector3 position) => Position = position;
        }
    }
}