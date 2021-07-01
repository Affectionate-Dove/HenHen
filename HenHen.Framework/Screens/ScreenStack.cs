// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Graphics2d.Layouts;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Screens
{
    public class ScreenStack : Drawable, IContainer<Screen>
    {
        private readonly List<Screen> screens = new();

        public Screen CurrentScreen => screens.Count == 0 ? null : screens[^1];

        public IEnumerable<Screen> Children => screens;

        public ContainerLayoutInfo ContainerLayoutInfo { get; private set; }

        public void Push(Screen screen)
        {
            var prev = CurrentScreen;
            screen.Parent = this;
            screens.Add(screen);
            RewireEventObserving(prev);
        }

        public void Pop()
        {
            if (screens.Count == 0)
                throw new InvalidOperationException("The screen stack is empty, cannot pop.");
            var prev = CurrentScreen;
            CurrentScreen.Parent = null;
            screens.Remove(CurrentScreen);
            RewireEventObserving(prev);
        }

        protected override void OnUpdate()
        {
            ContainerLayoutInfo = new ContainerLayoutInfo
            {
                ChildrenRenderPosition = ComputeChildrenRenderPosition(),
                ChildrenRenderSize = ComputeChildrenRenderSize()
            };
            base.OnUpdate();
            CurrentScreen?.Update();
        }

        protected override void OnRender()
        {
            base.OnRender();
            CurrentScreen?.Render();
        }

        private Vector2 ComputeChildrenRenderPosition() => LayoutInfo.RenderPosition;

        private Vector2 ComputeChildrenRenderSize() => LayoutInfo.RenderSize;

        private void RewireEventObserving(Screen previous)
        {
            if (previous is not null)
            {
                previous.Exited -= OnScreenExited;
                previous.ScreenPushed -= OnScreenPushed;
            }
            if (CurrentScreen is not null)
            {
                CurrentScreen.Exited += OnScreenExited;
                CurrentScreen.ScreenPushed += OnScreenPushed;
            }
        }

        private void OnScreenPushed(Screen nextScreen) => Push(nextScreen);

        private void OnScreenExited() => Pop();
    }
}