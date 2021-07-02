// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Input;
using System.Collections.Generic;

namespace HenHen.Framework.VisualTests.Input
{
    public class VisualTesterInputActionHandler : InputActionHandler<VisualTesterControls>
    {
        public VisualTesterInputActionHandler(Inputs inputs) : base(inputs)
        {
        }

        protected override Dictionary<VisualTesterControls, List<KeyboardKey>> CreateDefaultKeybindings() => new()
        {
            { VisualTesterControls.PreviousScene, new List<KeyboardKey> { KeyboardKey.KEY_PAGE_UP } },
            { VisualTesterControls.NextScene, new List<KeyboardKey> { KeyboardKey.KEY_PAGE_DOWN } }
        };
    }
}