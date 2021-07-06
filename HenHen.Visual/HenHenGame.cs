// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework;
using HenHen.Framework.Screens;
using HenHen.Visual.Screens.Main;

namespace HenHen.Visual
{
    public class HenHenGame : Game
    {
        public HenHenGame()
        {
            LoadImages();
            ScreenStack.Push(new MainMenuScreen());
        }

        private static void LoadImages() => TextureStore.Load("Images/logo.png");
    }
}