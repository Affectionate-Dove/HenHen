using HenHen.Framework.Input;
using System;
using System.Collections.Generic;

namespace HenHen.Framework.Tests.Input
{
    public class TestInputManager : InputManager
    {
        private readonly HashSet<KeyboardKey> justPressedKeys = new();
        private readonly HashSet<KeyboardKey> pressedKeys = new();

        public void SimulateKeyPress(KeyboardKey key)
        {
            justPressedKeys.Add(key);
            pressedKeys.Add(key);
        }

        public void SimulateKeyRelease(KeyboardKey key) => pressedKeys.Remove(key);

        public override bool IsKeyPressed(KeyboardKey key) => justPressedKeys.Contains(key);
        public override bool IsKeyDown(KeyboardKey key) => pressedKeys.Contains(key);
        public override bool IsKeyUp(KeyboardKey key) => !IsKeyDown(key);
        public override bool IsKeyReleased(KeyboardKey key) => throw new NotImplementedException();

        public override void Update(float timeDelta) => justPressedKeys.Clear();
    }
}