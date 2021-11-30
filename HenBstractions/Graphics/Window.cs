// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenBstractions.Graphics
{
    public class Window
    {
        private Vector2 size;

        private string title;

        private int targetFPS;

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

        public string Title
        {
            get => title;
            set
            {
                title = value;
                Raylib_cs.Raylib.SetWindowTitle(title);
            }
        }

        public int TargetFPS
        {
            get => targetFPS;
            set
            {
                targetFPS = value;
                Raylib_cs.Raylib.SetTargetFPS(targetFPS);
            }
        }

        public Window(Vector2 size, string title)
        {
            InitWindow(size, this.title = title);
            TargetFPS = 60;
        }

        protected void InitWindow(Vector2 size, string title)
        {
            Raylib_cs.Raylib.SetExitKey(Raylib_cs.KeyboardKey.KEY_NULL);
            var x = (int)size.X;
            var y = (int)size.Y;
            this.size = new Vector2(x, y);
            Raylib_cs.Raylib.InitWindow(x, y, title);
        }
    }
}