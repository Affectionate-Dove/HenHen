// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Input;
using HenHen.Framework.Screens;
using HenHen.Framework.VisualTests.Input;

namespace HenHen.Framework.VisualTests
{
    public abstract class VisualTestScene : Screen, IInputListener<SceneControls>
    {
        public bool IsSceneDone { get; protected set; }

        public VisualTestScene() => RelativeSizeAxes = Axes.Both;

        public virtual bool OnActionPressed(SceneControls action) => false;

        public virtual void OnActionReleased(SceneControls action)
        {
        }
    }
}