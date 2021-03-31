// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenHen.Framework.Input
{
    public interface IInputListener<TInputAction>
    {
        /// <summary>
        /// If <see cref="TInputAction"/> was handled, <see cref="OnActionPressed"/>
        /// should return <see cref="true"/>, otherwise <see cref="false"/>.
        /// </summary>
        bool OnActionPressed(TInputAction action);

        void OnActionReleased(TInputAction action);
    }
}