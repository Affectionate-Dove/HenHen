using System.Numerics;

namespace HenHen.Framework.Input
{
    public static class InputManager
    {
        public static Vector2 MousePosition
        {
            get => Raylib_cs.Raylib.GetMousePosition(); 
            set => Raylib_cs.Raylib.SetMousePosition((int)value.X, (int)value.Y);
        }

        public static float MouseWheelDelta => Raylib_cs.Raylib.GetMouseWheelMove();

        public static bool IsKeyDown(KeyboardKey key) => Raylib_cs.Raylib.IsKeyDown(key.ToRaylibKey());
        public static bool IsKeyUp(KeyboardKey key) => Raylib_cs.Raylib.IsKeyUp(key.ToRaylibKey());
        public static bool IsKeyPressed(KeyboardKey key) => Raylib_cs.Raylib.IsKeyPressed(key.ToRaylibKey());
        public static bool IsKeyReleased(KeyboardKey key) => Raylib_cs.Raylib.IsKeyReleased(key.ToRaylibKey());

        public static bool IsMouseButtonDown(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonDown(button.ToRaylibMouseButton());
        public static bool IsMouseButtonUp(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonUp(button.ToRaylibMouseButton());
        public static bool IsMouseButtonPressed(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonPressed(button.ToRaylibMouseButton());
        public static bool IsMouseButtonReleased(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonReleased(button.ToRaylibMouseButton());
    }
}
