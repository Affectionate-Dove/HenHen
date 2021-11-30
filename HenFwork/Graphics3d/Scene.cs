// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;

namespace HenFwork.Graphics3d
{
    /// <summary>
    ///     A 3D space that contains 3D objects (<seealso cref="Spatial"/>s).
    /// </summary>
    public class Scene
    {
        public List<Spatial> Spatials { get; } = new();
    }
}