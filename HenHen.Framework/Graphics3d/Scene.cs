// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;

namespace HenHen.Framework.Graphics3d
{
    public class Scene
    {
        public List<Spatial> Spatials { get; } = new();
        public Camera Camera { get; } = new();

        public void Render()
        {
            Raylib_cs.Raylib.BeginMode3D(Camera.RaylibCamera);
            foreach (var spatial in Spatials)
                spatial.Render();
            Raylib_cs.Raylib.EndMode3D();
        }
    }
}