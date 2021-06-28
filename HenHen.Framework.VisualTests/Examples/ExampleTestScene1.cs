// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.UI;
using System.Numerics;

namespace HenHen.Framework.VisualTests.Examples
{
    public class ExampleTestScene1 : VisualTestScene
    {
        public ExampleTestScene1() => AddChild(new ExampleDrawable() { Offset = new Vector2(50) });

        private class ExampleDrawable : Container
        {
            public ExampleDrawable()
            {
                AddChild(new Rectangle
                {
                    RelativeSizeAxes = Axes.Both,
                    Color = new ColorInfo(0, 60, 160)
                });
                AddChild(new SpriteText
                {
                    Text = "Sample Text 1",
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