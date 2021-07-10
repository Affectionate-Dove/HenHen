// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.VisualTests.Examples;
using HenHen.Framework.VisualTests.Input;

namespace HenHen.Framework.VisualTests.Graphics2d
{
    public class DrawableVisibilityTestScene : VisualTestScene
    {
        private readonly ExampleDrawable drawable;

        public DrawableVisibilityTestScene()
        {
            AddChild(drawable = new ExampleDrawable("Press 1 to change visibility")
            {
                Visible = true
            });
        }

        public override bool OnActionPressed(SceneControls action)
        {
            if (action != SceneControls.One)
                return base.OnActionPressed(action);

            drawable.Visible = !drawable.Visible;

            return true;
        }
    }
}