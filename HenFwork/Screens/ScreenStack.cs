// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Numerics;
using HenFwork.Graphics2d;
using HenFwork.Graphics2d.Layouts;
using System;
using System.Collections.Generic;

namespace HenFwork.Screens
{
    public class ScreenStack : Drawable, IContainer<Screen>
    {
        private readonly List<Screen> screens = new();

        public event Action<Screen> ScreenPushed;

        public event Action<Screen> ScreenPopped;

        public Screen CurrentScreen => screens.Count == 0 ? null : screens[^1];

        public IEnumerable<Screen> Children => screens;

        public ContainerLayoutInfo ContainerLayoutInfo { get; private set; }

        public void Push(Screen screen)
        {
            var prev = CurrentScreen;
            screen.Parent = this;
            screens.Add(screen);
            RewireEventObserving(prev);
            ScreenPushed?.Invoke(screen);
        }

        public void Pop()
        {
            if (screens.Count == 0)
                throw new InvalidOperationException("The screen stack is empty, cannot pop.");
            var prev = CurrentScreen;
            CurrentScreen.Parent = null;
            screens.Remove(CurrentScreen);
            RewireEventObserving(prev);
            ScreenPopped?.Invoke(prev);
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);
            ContainerLayoutInfo = new ContainerLayoutInfo
            {
                ChildrenRenderArea = ComputeChildrenRenderArea(),
                MaskArea = LayoutInfo.Mask
            };
            CurrentScreen?.Update(elapsed);
        }

        protected override void OnRender()
        {
            base.OnRender();
            CurrentScreen?.Render();
        }

        private RectangleF ComputeChildrenRenderArea() => LayoutInfo.RenderRect;

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