// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenHen.Framework.Input.UI
{
    public interface IInterfaceComponent<TInputAction> : IInputListener<TInputAction>
    {
        bool AcceptsFocus { get; }

        void OnFocus();

        void OnFocusLost();
    }
}