// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenFwork.Input
{
    public abstract class Inputs
    {
        public abstract float MouseWheelDelta { get; }
        public abstract Vector2 MousePosition { get; set; }

        public abstract bool IsKeyDown(KeyboardKey key);

        public abstract bool IsKeyUp(KeyboardKey key);

        public abstract bool IsMouseButtonDown(MouseButton button);

        public abstract bool IsMouseButtonUp(MouseButton button);
    }
}