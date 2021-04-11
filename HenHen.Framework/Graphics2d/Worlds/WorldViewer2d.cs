﻿// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.UI;
using HenHen.Framework.Worlds;
using System.Numerics;

namespace HenHen.Framework.Graphics2d.Worlds
{
    public class WorldViewer2d : Container
    {
        public World World { get; }

        public WorldViewer2d(World world)
        {
            AddChild(new Rectangle
            {
                Color = new ColorInfo(0, 60, 200),
                RelativeSizeAxes = Axes.Both
            });
            AddChild(new SpriteText
            {
                Text = "Placeholder",
                Anchor = new Vector2(0.5f),
                Origin = new Vector2(0.5f),
                RelativeSizeAxes = Axes.Both,
                AlignMiddle = true
            });
            World = world;
        }
    }
}