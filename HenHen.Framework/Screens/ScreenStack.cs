using HenHen.Framework.Graphics2d;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Screens
{
    public class ScreenStack : Drawable, IContainer<Screen>
    {
        private readonly List<Screen> screens = new List<Screen>();

        public Screen CurrentScreen => screens.Count == 0 ? null : screens[^1];

        public IEnumerable<Screen> Children => screens;

        public Vector2 GetChildrenRenderPosition() => GetRenderPosition();
        public Vector2 GetChildrenRenderSize() => GetRenderSize();

        protected override void OnUpdate()
        {
            base.OnUpdate();
            CurrentScreen?.Update();
        }

        protected override void OnRender()
        {
            base.OnRender();
            CurrentScreen?.Render();
        }

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
