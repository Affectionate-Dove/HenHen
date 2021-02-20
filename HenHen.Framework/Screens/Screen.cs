using HenHen.Framework.Graphics2d;
using System;

namespace HenHen.Framework.Screens
{
    public class Screen : Container, IContainer<Drawable>
    {
        public event Action<Screen> ScreenPushed;
        public event Action Exited;

        public Screen() => RelativeSizeAxes = Axes.Both;

        public void Push(Screen nextScreen) => ScreenPushed?.Invoke(nextScreen);

        public void Exit() => Exited?.Invoke();
    }
}
