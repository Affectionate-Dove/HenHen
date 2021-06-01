// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.UI;
using System.Numerics;

namespace HenHen.Framework.VisualTests.Examples
{
    public class ExampleTestScene2 : VisualTestScene
    {
        public ExampleTestScene2() => AddChild(new ExampleDrawable() { Offset = new Vector2(150, 50) });

        private class ExampleDrawable : Container
        {
            public ExampleDrawable()
            {
                AddChild(new Rectangle
                {
                    RelativeSizeAxes = Axes.Both,
                    Color = new ColorInfo(200, 60, 30)
                });
                AddChild(new SpriteText
                {
                    Text = "Sample Text 2",
                    Anchor = new Vector2(0.5f),
                    Origin = new Vector2(0.5f),
                    RelativeSizeAxes = Axes.Both,
                    AlignMiddle = true
                });
                Size = new Vector2(300);
            }
        }
    }
}