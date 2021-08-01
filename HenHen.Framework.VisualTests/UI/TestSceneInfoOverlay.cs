// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.UI;
using System;
using System.Linq;

namespace HenHen.Framework.VisualTests.UI
{
    public class TestSceneInfoOverlay : Container
    {
        private readonly FillFlowContainer flowContainer;
        private readonly Rectangle background;

        public TestSceneInfoOverlay()
        {
            // background
            AddChild(background = new Rectangle { RelativeSizeAxes = Axes.Both, Color = Raylib_cs.Color.DARKGRAY });

            AddChild(flowContainer = new FillFlowContainer
            {
                AutoSizeAxes = Axes.Y,
                RelativeSizeAxes = Axes.X,
                Direction = Direction.Vertical,
                Padding = new() { Horizontal = 10, Vertical = 10 },
                Spacing = 10
            });
        }

        public void ChangeScene(VisualTestScene testScene)
        {
            foreach (var child in flowContainer.Children.ToList()) // TODO
                flowContainer.RemoveChild(child);

            if (testScene.Description is not null)
            {
                flowContainer.AddChild(new SpriteText { Text = "Description", FontSize = 20 });
                flowContainer.AddChild(new SpriteText { Text = testScene.Description });
            }

            if (testScene.ControlsDescriptions is not null && testScene.ControlsDescriptions.Count > 0)
            {
                flowContainer.AddChild(new SpriteText { Text = "Controls", FontSize = 20 });
                foreach (var (controls, description) in testScene.ControlsDescriptions)
                    flowContainer.AddChild(new SpriteText { Text = $"{string.Join(", ", controls)}: {description}" });
            }
        }

        protected override void OnLayoutUpdate()
        {
            base.OnLayoutUpdate();
            Console.WriteLine($"({DateTime.Now}) Overlay size:{LayoutInfo.RenderSize}\nRectangle size: {background.LayoutInfo.RenderSize}");
        }
    }
}