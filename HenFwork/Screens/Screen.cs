// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenFwork.Graphics2d;
using System;

namespace HenFwork.Screens
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