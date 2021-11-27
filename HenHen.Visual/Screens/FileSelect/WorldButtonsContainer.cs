// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using System;

namespace HenHen.Visual.Screens.FileSelect
{
    /// <summary>
    ///     Manages world selection interface with buttons.
    /// </summary>
    public class WorldButtonsContainer : Container
    {
        public event Action/*<World>*/ WorldSelected;

        public WorldButtonsContainer(/*IEnumerable<World> worlds*/)
        {
            AddChild(new Rectangle
            {
                Color = new(20, 20, 20, 255),
                RelativeSizeAxes = Axes.Both
            });
            Masking = true;
            var flow = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Direction = Direction.Vertical,
                Spacing = 5,
                Padding = new MarginPadding { Horizontal = 15, Vertical = 15 }
            };
            AddChild(flow);
            for (var i = 1; i <= 3; i++)
            {
                var worldButton = new WorldButton($"World {i}")
                {
                    RelativeSizeAxes = Axes.X,
                    Size = new(1, 100),
                    Action = () => WorldSelected?.Invoke()
                };
                worldButton.AcceptedActions.Add(Inputs.MenuActions.Confirm);
                flow.AddChild(worldButton);
            }
        }
    }
}