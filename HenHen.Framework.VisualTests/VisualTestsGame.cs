// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenHen.Framework.VisualTests
{
    public class VisualTestsGame : Game
    {
        public VisualTestsGame() => ScreenStack.Push(new VisualTester());
    }
}