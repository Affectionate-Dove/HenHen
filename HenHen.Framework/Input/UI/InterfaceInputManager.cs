// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Screens;
using System.Collections.Generic;

namespace HenHen.Framework.Input.UI
{
    public class InterfaceInputManager<TInputAction> : IInputListener<TInputAction> where TInputAction : System.Enum
    {
        private readonly Stack<ContainerAndEnumerator> stack = new();
        private readonly InputPropagator<TInputAction> inputPropagator;

        public ScreenStack ScreenStack { get; }
        public IInterfaceComponent<TInputAction> CurrentComponent { get; private set; }
        public TInputAction NextComponentAction { get; set; }

        public InterfaceInputManager(ScreenStack screenStack) => ScreenStack = screenStack;

        public InterfaceInputManager(ScreenStack screenStack, TInputAction nextComponentAction) : this(screenStack)
        {
            NextComponentAction = nextComponentAction;
            inputPropagator = new();
            inputPropagator.Listeners.Add(new NextComponentActionListener(this));
        }

        public void FocusNextComponent()
        {
            var previouslyFocusedComponent = CurrentComponent;

            var restartCount = 0;
            while (true)
            {
                if (RestartStackIfNeeded())
                    restartCount++;
                if (restartCount == 2)
                    return;

                var enumerator = stack.Peek().Enumerator;
                if (HandleEnumerator(enumerator))
                {
                    inputPropagator?.Listeners.Remove(previouslyFocusedComponent);
                    previouslyFocusedComponent?.OnFocusLost();

                    inputPropagator?.Listeners.Add(CurrentComponent);
                    CurrentComponent.OnFocus();

                    return;
                }
            };
        }

        public bool OnActionPressed(TInputAction action) => inputPropagator.OnActionPressed(action);

        public void OnActionReleased(TInputAction action) => inputPropagator.OnActionReleased(action);

        private bool HandleEnumerator(IEnumerator<Drawable> enumerator)
        {
            try
            {
                if (enumerator.MoveNext())
                {
                    if (HandleDrawable(enumerator.Current))
                        return true;
                }
                else
                    stack.Pop();
            }
            catch (System.InvalidOperationException)
            {
                stack.Pop();
            }
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

        private bool HandleDrawable(Drawable drawable)
        {
            if (drawable is Container container)
                stack.Push(new(container));
            if (drawable is IInterfaceComponent<TInputAction> component)
            {
                if (!component.AcceptsFocus)
                    return false;

                CurrentComponent = component;
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

            public bool OnActionPressed(TInputAction action) => true;

            public void OnActionReleased(TInputAction action) => interfaceInputManager.FocusNextComponent();
        }
    }
}