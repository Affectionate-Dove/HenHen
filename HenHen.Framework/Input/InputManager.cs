using System.Numerics;

namespace HenHen.Framework.Input
{
    public class InputManager
    {
        private Vector2? mousePosition;

        public virtual Vector2? LastMousePosition { get; protected set; }

        public virtual Vector2 MousePosition
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

        public virtual bool IsKeyDown(KeyboardKey key) => Raylib_cs.Raylib.IsKeyDown(key.ToRaylibKey());
        public virtual bool IsKeyUp(KeyboardKey key) => Raylib_cs.Raylib.IsKeyUp(key.ToRaylibKey());
        public virtual bool IsKeyPressed(KeyboardKey key) => Raylib_cs.Raylib.IsKeyPressed(key.ToRaylibKey());
        public virtual bool IsKeyReleased(KeyboardKey key) => Raylib_cs.Raylib.IsKeyReleased(key.ToRaylibKey());

        public virtual bool IsMouseButtonDown(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonDown(button.ToRaylibMouseButton());
        public virtual bool IsMouseButtonUp(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonUp(button.ToRaylibMouseButton());
        public virtual bool IsMouseButtonPressed(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonPressed(button.ToRaylibMouseButton());
        public virtual bool IsMouseButtonReleased(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonReleased(button.ToRaylibMouseButton());

        public virtual void Update(float timeDelta)
        {
            LastMousePosition = mousePosition;
            MousePosition = Raylib_cs.Raylib.GetMousePosition();
            TimeDelta = timeDelta;
        }
    }
}
