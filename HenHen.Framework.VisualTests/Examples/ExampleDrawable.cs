// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.UI;
using System.Numerics;

namespace HenHen.Framework.VisualTests.Examples
{
    public class ExampleDrawable : Container, IHasColor
    {
        private readonly Rectangle rectangle;

        public ColorInfo Color { get => rectangle.Color; set => rectangle.Color = value; }

        public ExampleDrawable(string text = "Sample text")
        {
            AddChild(rectangle = new Rectangle
            {
                RelativeSizeAxes = Axes.Both,
                Color = new ColorInfo(0, 0, 0)
            });
            AddChild(new SpriteText
            {
                Text = text,
                Anchor = new Vector2(0.5f),
                Origin = new Vector2(0.5f),
                RelativeSizeAxes = Axes.Both,
                TextAlignment = new(0.5f)
            });
            Size = new Vector2(300);
        }
    }
}