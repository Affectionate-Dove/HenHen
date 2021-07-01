// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenHen.Framework.Input.UI
{
    /// <summary>
    ///     A component that can get focused for input.
    /// </summary>
    public interface IInterfaceComponent<TInputAction> : IInputListener<TInputAction>
    {
        /// <summary>
        ///     Whether this component can be focused.
        /// </summary>
        bool AcceptsFocus { get; }

        /// <summary>
        ///     Called when this component gets focused.
        /// </summary>
        void OnFocus();

        /// <summary>
        ///     Called when this component loses focus.
        /// </summary>
        void OnFocusLost();
    }
}