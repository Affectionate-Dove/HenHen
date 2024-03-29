﻿// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenFwork;
using HenFwork.Input.UI;
using HenFwork.Screens;
using HenHen.Visual.Inputs;
using HenHen.Visual.Screens.Main;

namespace HenHen.Visual
{
    public class HenHenGame : Game
    {
        private readonly MenuActionsHandler menuActionsHandler;
        private readonly InterfaceInputManager<MenuActions> interfaceInputManager;
        private readonly PositionalInterfaceInputManager positionalInterfaceInputManager;

        public HenHenGame()
        {
            LoadImages();
            menuActionsHandler = new(Inputs);
            interfaceInputManager = new(ScreenStack, MenuActions.Next);
            menuActionsHandler.Propagator.Listeners.Add(interfaceInputManager);
            positionalInterfaceInputManager = new(Inputs, ScreenStack);
            ScreenStack.Push(new MainMenuScreen());
        }

        protected override void OnUpdate()
        {
            menuActionsHandler.Update();
            positionalInterfaceInputManager.Update();
            base.OnUpdate();
        }

        private static void LoadImages()
        {
            TextureStore.Load("Images/logo.png");
            TextureStore.Load("Images/Backgrounds/MainMenu1.png").GenerateMipmaps();
        }
    }
}