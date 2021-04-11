// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Screens;

namespace HenHen.Framework.VisualTests
{
    public abstract class VisualTestScene : Screen
    {
        public bool IsSceneDone { get; protected set; }

        public VisualTestScene() => RelativeSizeAxes = Axes.Both;
    }
}