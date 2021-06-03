// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d.Worlds;
using HenHen.Framework.Numerics;
using HenHen.Framework.Worlds;
using HenHen.Framework.Worlds.Mediums;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.VisualTests.Graphics2d.Worlds
{
    public class WorldViewer2dTestScene : VisualTestScene
    {
        public WorldViewer2dTestScene()
        {
            var world = new World(new Vector2(10), 2);
            foreach (var medium in GetSampleMediums())
            {
                world.AddMedium(medium);
            }
            var worldViewer2d = new WorldViewer2d(world)
            {
                Size = new Vector2(200),
                Anchor = new Vector2(0.5f),
                Origin = new Vector2(0.5f)
            };
            AddChild(worldViewer2d);
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
    }
}