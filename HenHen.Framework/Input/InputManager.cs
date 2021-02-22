using System.Numerics;

namespace HenHen.Framework.Input
{
    public class InputManager
    {
        private Vector2? mousePosition;

        public Vector2? LastMousePosition { get; protected set; }

        public Vector2 MousePosition
        {
            get
            {
                if (mousePosition.HasValue)
                    return mousePosition.Value;
                return (mousePosition = Raylib_cs.Raylib.GetMousePosition()).Value;
            }
            set
            {
                mousePosition = value;
                Raylib_cs.Raylib.SetMousePosition((int)value.X, (int)value.Y);
            }
        }

        /// <summary>
        /// Returns Vector2.Zero if there is no delta.
        /// </summary>
        public Vector2 MousePositionDelta => (MousePosition - LastMousePosition).GetValueOrDefault();

        public float TimeDelta { get; protected set; }

        public static float MouseWheelDelta => Raylib_cs.Raylib.GetMouseWheelMove();

        public static bool IsKeyDown(KeyboardKey key) => Raylib_cs.Raylib.IsKeyDown(key.ToRaylibKey());
        public static bool IsKeyUp(KeyboardKey key) => Raylib_cs.Raylib.IsKeyUp(key.ToRaylibKey());
        public static bool IsKeyPressed(KeyboardKey key) => Raylib_cs.Raylib.IsKeyPressed(key.ToRaylibKey());
        public static bool IsKeyReleased(KeyboardKey key) => Raylib_cs.Raylib.IsKeyReleased(key.ToRaylibKey());

        public static bool IsMouseButtonDown(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonDown(button.ToRaylibMouseButton());
        public static bool IsMouseButtonUp(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonUp(button.ToRaylibMouseButton());
        public static bool IsMouseButtonPressed(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonPressed(button.ToRaylibMouseButton());
        public static bool IsMouseButtonReleased(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonReleased(button.ToRaylibMouseButton());

        public void Update(float timeDelta)
        {
            LastMousePosition = mousePosition;
            MousePosition = Raylib_cs.Raylib.GetMousePosition();
            TimeDelta = timeDelta;
        }
    }
}
