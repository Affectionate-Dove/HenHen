// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Input;
using System.Collections.Generic;

namespace HenHen.Visual.Inputs
{
    public class MenuActionsHandler : InputActionHandler<MenuActions>
    {
        public MenuActionsHandler(Framework.Input.Inputs inputs) : base(inputs)
        {
        }

        protected override Dictionary<MenuActions, ISet<KeyboardKey>> CreateDefaultKeybindings() => new()
        {
            { MenuActions.Next, new HashSet<KeyboardKey> { KeyboardKey.KEY_TAB } },
            { MenuActions.Confirm, new HashSet<KeyboardKey> { KeyboardKey.KEY_ENTER } },
            { MenuActions.Cancel, new HashSet<KeyboardKey> { KeyboardKey.KEY_ESCAPE } },
        };
    }
}