// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;
using System.Collections.Generic;

namespace HenHen.Framework.Input
{
    /// <summary>
    /// Input propagator propagates <see cref="TInputAction"/>s
    /// to <see cref="IInputListener"/>s in the
    /// <see cref="Listeners"/> list.
    /// </summary>
    public class InputPropagator<TInputAction> where TInputAction : Enum
    {
        private readonly Dictionary<TInputAction, IInputListener<TInputAction>> triggeredListeners = new();

        /// <remarks>
        /// Use <see cref="ReleaseActiveActions(IInputListener{TInputAction})"/>
        /// when removing a listener to release all its active actions.
        /// </remarks>
        public List<IInputListener<TInputAction>> Listeners { get; } = new();

        public InputPropagator()
        {
            foreach (TInputAction enumValue in Enum.GetValues(typeof(TInputAction)))
                triggeredListeners.Add(enumValue, null);
        }

        public bool OnActionPressed(TInputAction action)
        {
            for (var i = Listeners.Count - 1; i >= 0; i--)
            {
                if (Listeners[i].OnActionPressed(action))
                {
                    triggeredListeners[action] = Listeners[i];
                    return true;
                }
            }
            triggeredListeners[action] = null;
            return false;
        }

        public void OnActionReleased(TInputAction action)
        {
            var listenerToNotify = triggeredListeners[action];
            if (listenerToNotify is null)
                return;

            // trigger that listener only if it's still registered
            if (Listeners.Contains(listenerToNotify))
                listenerToNotify.OnActionReleased(action);

            triggeredListeners[action] = null;
        }

        public void ReleaseActiveActions(IInputListener<TInputAction> listener)
        {
            foreach (var action in triggeredListeners.Keys)
            {
                var triggeredListener = triggeredListeners[action];
                if (triggeredListener == listener)
                {
                    listener.OnActionReleased(action);
                    triggeredListeners[action] = null;
                }
            }
        }
    }
}