// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.UI;
using HenHen.Visual.Inputs;

namespace HenHen.Visual.Screens.FileSelect
{
    public class WorldButton : Button<MenuActions>
    {
        private readonly Rectangle square;
        private readonly SpriteText worldNameSpriteText;

        public WorldButton(string worldName)
        {
            var flow = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Direction = Direction.Horizontal,
                Spacing = 4,
                Padding = new() { Horizontal = 10, Vertical = 10 }
            };
            flow.AddChild(square = new Rectangle
            {
                RelativeSizeAxes = Axes.Y,
                Color = new ColorInfo(10, 100, 200),
            });
            flow.AddChild(worldNameSpriteText = new SpriteText
            {
                Text = worldName,
            });
            AddChild(flow);
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);
            square.Size = new(square.LayoutInfo.RenderSize.Y, 1);
            worldNameSpriteText.Offset = new(square.LayoutInfo.RenderSize.Y + 10, 1);
        }
    }
}