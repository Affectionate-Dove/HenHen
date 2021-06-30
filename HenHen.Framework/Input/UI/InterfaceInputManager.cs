// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Screens;
using System;
using System.Collections.Generic;

namespace HenHen.Framework.Input.UI
{
    /// <summary>
    ///     Handles user interface components'
    ///     (<seealso cref="IInterfaceComponent{TInputAction}"/>)
    ///     focus and input.
    /// </summary>
    public class InterfaceInputManager<TInputAction> : IInputListener<TInputAction> where TInputAction : struct, Enum
    {
        private readonly Stack<ContainerAndEnumerator> stack = new();
        private readonly InputPropagator<TInputAction> inputPropagator;

        /// <summary>
        ///     The <see cref="Screens.ScreenStack"/>, inside
        ///     of which all handled interface components are.
        /// </summary>
        public ScreenStack ScreenStack { get; }

        /// <summary>
        ///     The <seealso cref="IInterfaceComponent{TInputAction}"/>
        ///     that currently has focus.
        /// </summary>
        // TODO: On each get, validate whether the component
        // is still available in the drawable tree.
        public IInterfaceComponent<TInputAction> CurrentlyFocusedComponent { get; private set; }

        /// <summary>
        ///     The <typeparamref name="TInputAction"/> that
        ///     triggers the <see cref="FocusNextComponent"/> function.
        /// </summary>
        public TInputAction? NextComponentAction { get; set; }

        public InterfaceInputManager(ScreenStack screenStack)
        {
            ScreenStack = screenStack;
            inputPropagator = new();
            inputPropagator.Listeners.Add(new NextComponentActionListener(this));
        }

        public InterfaceInputManager(ScreenStack screenStack, TInputAction nextComponentAction) : this(screenStack) => NextComponentAction = nextComponentAction;

        /// <summary>
        ///     Finds the next <see cref="IInterfaceComponent{TInputAction}"/>
        ///     in the <see cref="Drawable"/> tree inside of <see cref="ScreenStack"/>
        ///     and focuses it.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Sets <seealso cref="CurrentlyFocusedComponent"/> to the found component,
        ///         calls its <see cref="IInterfaceComponent{TInputAction}.OnFocus"/> function,
        ///         and, if it exists, calls previously focused component's
        ///         <see cref="IInterfaceComponent{TInputAction}.OnFocusLost"/>.
        ///     </para>
        ///     <para>
        ///         When a <see cref="Container"/> of interface components is also
        ///         a <see cref="IInterfaceComponent{TInputAction}"/> itself,
        ///         the container gets focused first, without focusing any children.
        ///         Then, when it loses focus, the focus goes to its children one by one.
        ///         After all children lost focus, the next
        ///         component after the container gets the focus.
        ///     </para>
        /// </remarks>
        public void FocusNextComponent() => FocusNextComponent(null);

        /// <summary>
        ///     Removes focus from <see cref="CurrentlyFocusedComponent"/>.
        /// </summary>
        /// <remarks>
        ///     The <see cref="FocusNextComponent"/> function will begin
        ///     seeking from the beginning the next time it's called.
        /// </remarks>
        public void Unfocus()
        {
            stack.Clear();
            DoUnfocusTasks(CurrentlyFocusedComponent);
            CurrentlyFocusedComponent = null;
            inputPropagator?.Listeners.RemoveAll(listener => listener is not NextComponentActionListener);
        }

        /// <summary>
        ///     Focuses the <paramref name="component"/>.
        /// </summary>
        /// <remarks>
        ///     Internally calls <see cref="FocusNextComponent"/>
        ///     until it finds the <paramref name="component"/>.
        /// </remarks>
        /// <param name="component">
        ///     The component to focus.
        /// </param>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if the <paramref name="component"/> isn't inside the
        ///     <see cref="ScreenStack"/>'s <see cref="Drawable"/> tree.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the <paramref name="component"/> is null.
        /// </exception>
        public void FocusComponent(IInterfaceComponent<TInputAction> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));
            FocusNextComponent(component);
        }

        /// <summary>
        ///     Propagates the <paramref name="action"/> press
        ///     to the <see cref="CurrentlyFocusedComponent"/>.
        /// </summary>
        /// <remarks>
        ///     If the <paramref name="action"/> is equal to <see cref="NextComponentAction"/>,
        ///     and the <see cref="CurrentlyFocusedComponent"/> didn't handle it,
        ///     this <see cref="InterfaceInputManager{TInputAction}"/> will handle this press,
        ///     and on <paramref name="action"/> release,
        ///     the <see cref="FocusNextComponent"/> function will be called.
        /// </remarks>
        /// <returns>
        ///     Whether the <see cref="CurrentlyFocusedComponent"/> or
        ///     this <see cref="InterfaceInputManager{TInputAction}"/>
        ///     handled the <paramref name="action"/>.
        /// </returns>
        public bool OnActionPressed(TInputAction action) => inputPropagator.OnActionPressed(action);

        /// <summary>
        ///     Propagates the <paramref name="action"/> release
        ///     to the <see cref="CurrentlyFocusedComponent"/>,
        ///     or calls the <see cref="FocusNextComponent"/> function.
        /// </summary>
        public void OnActionReleased(TInputAction action) => inputPropagator.OnActionReleased(action);

        private void FocusNextComponent(IInterfaceComponent<TInputAction> componentToFocus)
        {
            var previouslyFocusedComponent = CurrentlyFocusedComponent;

            var restartCount = 0;
            while (true)
            {
                if (RestartStackIfNeeded())
                    restartCount++;
                if (restartCount == 2)
                    break;

                if (HandleEnumerator())
                {
                    if (componentToFocus is null || componentToFocus == CurrentlyFocusedComponent)
                    {
                        DoUnfocusTasks(previouslyFocusedComponent);
                        DoFocusTasks(CurrentlyFocusedComponent);
                        return;
                    }
                }
            };

            if (componentToFocus is not null)
                throw new InvalidOperationException($"The requested {nameof(componentToFocus)} is not inside the {nameof(Drawable)} tree of the {nameof(ScreenStack)}.");
        }

        private void DoUnfocusTasks(IInterfaceComponent<TInputAction> previouslyFocusedComponent)
        {
            inputPropagator?.Listeners.Remove(previouslyFocusedComponent);
            previouslyFocusedComponent?.OnFocusLost();
        }

        private void DoFocusTasks(IInterfaceComponent<TInputAction> component)
        {
            inputPropagator?.Listeners.Add(component);
            component.OnFocus();
        }

        /// <summary>
        ///     Advances the <see cref="IEnumerator{Drawable}"/>
        ///     at the top of the <see cref="stack"/> to the next <see cref="Drawable"/>.
        ///     If that's successful, calls and returns
        ///     the return value of <see cref="HandleDrawable(Drawable)"/>.
        ///     If not, pops the <see cref="stack"/> and returns false.
        /// </summary>
        /// <param name="enumerator"></param>
        /// <returns>
        ///     Whether advancing the <see cref="IEnumerator{Drawable}"/>
        ///     at the top of the <see cref="stack"/> resulted in finding
        ///     a <see cref="IInterfaceComponent{TInputAction}"/>.
        /// </returns>
        private bool HandleEnumerator()
        {
            var enumerator = stack.Peek().Enumerator;
            try
            {
                if (enumerator.MoveNext())
                    return HandleDrawable(enumerator.Current);
            }
            catch (InvalidOperationException)
            {
            }

            stack.Pop();
            return false;
        }

        private bool RestartStackIfNeeded()
        {
            if (stack.Count == 0)
            {
                stack.Push(new(ScreenStack.CurrentScreen));
                return true;
            }

            return false;
        }

        /// <summary>
        ///     <para>
        ///         If the <paramref name="drawable"/> is a <see cref="Container"/>,
        ///         pushes it to the <see cref="stack"/>.
        ///     </para>
        ///     <para>
        ///         If the <paramref name="drawable"/>
        ///         is a <see cref="IInterfaceComponent{TInputAction}"/>
        ///         and it <see cref="IInterfaceComponent{TInputAction}.AcceptsFocus"/>,
        ///         sets <see cref="CurrentlyFocusedComponent"/> to it.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     Whether a new <see cref="IInterfaceComponent{TInputAction}"/>
        ///     was set as <see cref="CurrentlyFocusedComponent"/>.
        /// </returns>
        private bool HandleDrawable(Drawable drawable)
        {
            if (drawable is Container container)
                stack.Push(new(container));
            if (drawable is IInterfaceComponent<TInputAction> component && component.AcceptsFocus)
            {
                CurrentlyFocusedComponent = component;
                return true;
            }
            return false;
        }

        private struct ContainerAndEnumerator
        {
            public Container Container;
            public IEnumerator<Drawable> Enumerator;

            public ContainerAndEnumerator(Container container)
            {
                Container = container;
                Enumerator = container.Children.GetEnumerator();
            }
        }

        private class NextComponentActionListener : IInputListener<TInputAction>
        {
            private readonly InterfaceInputManager<TInputAction> interfaceInputManager;

            public NextComponentActionListener(InterfaceInputManager<TInputAction> interfaceInputManager) => this.interfaceInputManager = interfaceInputManager;

            public bool OnActionPressed(TInputAction action) => action.Equals(interfaceInputManager.NextComponentAction);

            public void OnActionReleased(TInputAction action) => interfaceInputManager.FocusNextComponent();
        }
    }
}