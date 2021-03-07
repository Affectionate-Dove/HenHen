// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;

namespace HenHen.Framework.Graphics3d
{
    public class Scene
    {
        public List<Spatial> Spatials { get; } = new();

        public void Render()
        {
            foreach (var spatial in Spatials)
                spatial.Render();
        }
    }
}