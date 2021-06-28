// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.UI;
using System.Collections.Generic;

namespace HenHen.Framework.VisualTests.Input
{
    public class SceneInputTestScene : VisualTestScene
    {
        private readonly List<SceneControls> pressedControls = new();
        private readonly SpriteText spriteText;

        public SceneInputTestScene()
        {
            AddChild(spriteText = new SpriteText { RelativeSizeAxes = Axes.Both, AlignMiddle = true });
            UpdateText();
        }

        public override bool OnActionPressed(SceneControls action)
        {
            pressedControls.Add(action);
            UpdateText();
            return true;
        }

        public override void OnActionReleased(SceneControls action)
        {
            pressedControls.Remove(action);
            UpdateText();
        }

        private void UpdateText() => spriteText.Text = pressedControls.Count == 0 ? "Nothing pressed" : string.Join(", ", pressedControls);
    }
}