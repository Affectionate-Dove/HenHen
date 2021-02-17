using HenHen.Framework.Graphics;
using System;

namespace HenHen.Framework.Screens
{
    public class Screen : Container, IContainer<Drawable>
    {
        public event Action<Screen> ScreenPushed;
        public event Action Exited;

        public void Push(Screen nextScreen) => ScreenPushed?.Invoke(nextScreen);

        public void Exit() => Exited?.Invoke();
    }
}
