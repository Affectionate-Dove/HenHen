// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenFwork.Input.UI;
using HenFwork.Screens;
using HenHen.Visual.Inputs;
using System;

namespace HenHen.Visual.Screens
{
    /// <summary>
    ///     A <see cref="Screen"/> with implemented
    ///     exiting on <see cref="MenuActions.Cancel"/> action.
    /// </summary>
    public class HenHenScreen : Screen, IInterfaceComponent<MenuActions>
    {
        public event Action<IInterfaceComponent<MenuActions>> FocusRequested { add { } remove { } }

        public bool AcceptsFocus => true;

        public bool OnActionPressed(MenuActions action) => action == MenuActions.Cancel;

        public void OnActionReleased(MenuActions action)
        {
            if (action == MenuActions.Cancel)
                Exit();
        }

        public void OnFocus()
        {
        }

        public void OnFocusLost()
        {
        }
    }
}