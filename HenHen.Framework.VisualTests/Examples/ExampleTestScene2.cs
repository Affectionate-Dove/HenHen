// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.UI;
using HenHen.Framework.VisualTests.Input;
using System.Numerics;

namespace HenHen.Framework.VisualTests.Examples
{
    public class ExampleTestScene2 : VisualTestScene
    {
        private readonly ExampleDrawable drawable;

        public ExampleTestScene2()
        {
            AddChild(drawable = new ExampleDrawable("Sample text 2")
            {
                Offset = new Vector2(150, 50),
                Color = new(200, 60, 30)
            });
            AddChild(new SpriteText
            {
                Anchor = new(1),
                Origin = new(1),
                Text = "Press 1 to change the drawable's color.",
                TextAlignment = Vector2.One
            });
        }

        public override bool OnActionPressed(SceneControls action)
        {
            if (action != SceneControls.One)
                return base.OnActionPressed(action);

            if (drawable.Color.Equals((ColorInfo)Raylib_cs.Color.DARKGREEN))
                drawable.Color = new(200, 60, 30);
            else
                drawable.Color = Raylib_cs.Color.DARKGREEN;

            return true;
        }
    }
}