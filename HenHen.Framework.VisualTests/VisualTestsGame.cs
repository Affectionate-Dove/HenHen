﻿// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Input.UI;
using HenHen.Framework.VisualTests.Input;

namespace HenHen.Framework.VisualTests
{
    public class VisualTestsGame : Game
    {
        private readonly VisualTesterInputActionHandler inputActionHandler;
        private readonly PositionalInterfaceInputManager positionalInterfaceInputManager;

        public VisualTestsGame()
        {
            var visualTesterScreen = new VisualTester(Inputs);

            inputActionHandler = new VisualTesterInputActionHandler(Inputs);
            inputActionHandler.Propagator.Listeners.Add(visualTesterScreen);

            positionalInterfaceInputManager = new(Inputs, ScreenStack);

            ScreenStack.Push(visualTesterScreen);
        }

        protected override void OnUpdate()
        {
            inputActionHandler.Update();
            positionalInterfaceInputManager.Update();
            base.OnUpdate();
        }
    }
}