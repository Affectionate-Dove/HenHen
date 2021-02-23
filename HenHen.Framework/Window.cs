using System.Numerics;

namespace HenHen.Framework
{
    public class Window
    {
        public Window(Vector2 size, string title)
        {
            InitWindow(size, title);
            TargetFPS = 60;
        }

        protected void InitWindow(Vector2 size, string title)
        {
            var x = (int)size.X;
            var y = (int)size.Y;
            this.size = new Vector2(x, y);
            Raylib_cs.Raylib.InitWindow(x, y, title);
        }

        private Vector2 size;
        public Vector2 Size
        {
            get => size;
            set
            {
                var x = (int)value.X;
                var y = (int)value.Y;
                size = new Vector2(x, y);
                Raylib_cs.Raylib.SetWindowSize(x, y);
            }
        }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                Raylib_cs.Raylib.SetWindowTitle(title);
            }
        }

        private int targetFPS;
        public int TargetFPS
        {
            get => targetFPS;
            set
            {
                targetFPS = value;
                Raylib_cs.Raylib.SetTargetFPS(targetFPS);
            }
        }
    }
}
