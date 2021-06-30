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

        public WorldViewer2dTestScene()
        {
            var world = new World(new Vector2(10), 2);
            AddNodes(world);
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
            if (action == SceneControls.One)
            {
                worldViewer2d.GridDistance -= 0.5f;
                return true;
            }
            else if (action == SceneControls.Two)
            {
                worldViewer2d.GridDistance += 0.5f;
                return true;
            }
            else if (action == SceneControls.Up)
            {
                worldViewer2d.Target += new Vector2(0, 1);
                return true;
            }
            else if (action == SceneControls.Down)
            {
                worldViewer2d.Target -= new Vector2(0, 1);
                return true;
            }
            else if (action == SceneControls.Left)
            {
                worldViewer2d.Target -= new Vector2(1, 0);
                return true;
            }
            else if (action == SceneControls.Right)
            {
                worldViewer2d.Target += new Vector2(1, 0);
                return true;
            }
            return base.OnActionPressed(action);
        }

        private static void AddNodes(World world)
        {
            var node1 = new TestNode()
            {
                Position = new Vector3(1, 0, 1)
            };
            var node2 = new TestNode()
            {
                Position = new Vector3(0, 4, 5)
            };
            var node3 = new TestNode()
            {
                Position = new Vector3(4, 2, 0)
            };
            world.AddNode(node1);
            world.AddNode(node2);
            world.AddNode(node3);
        }

        private static IEnumerable<Medium> GetSampleMediums() => new Medium[]
        {
            new Medium
            {
                Triangle = new Triangle3
                {
                    A = new(-5, 0, 5),
                    B = new(0, 0, -5),
                    C = new(0, 0, 5)
                },
                Type = MediumType.Water
            },
            new Medium
            {
                Triangle = new Triangle3
                {
                    A = new(-5, 0, 5),
                    B = new(-5, 0, -5),
                    C = new(0, 0, -5)
                },
                Type = MediumType.Water
            },
            new Medium
            {
                Triangle = new Triangle3
                {
                    A = new(0, 0, -5),
                    B = new(2, 0, -5),
                    C = new(2, 0, 5)
                },
                Type = MediumType.Ground
            },
            new Medium
            {
                Triangle = new Triangle3
                {
                    A = new(0, 0, -5),
                    B = new(2, 0, 5),
                    C = new(0, 0, 5)
                },
                Type = MediumType.Ground
            },
        };

        private static SpriteText GenerateInfoText()
        {
            var t =
                "1 - decrease grid spacing by 0.5\n" +
                "2 - increase grid spacing by 0.5\n" +
                "↑/↓/←/→ - move view";

            return new()
            {
                Anchor = new(1, 1),
                Origin = new(1, 1),
                Size = new(300, 600),
                Text = t
            };
        }

        private class TestNode : Node
        {
        }
    }
}