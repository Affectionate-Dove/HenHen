// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenHen.Framework.Graphics3d
{
    public abstract class Spatial
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public void Render() => OnRender();

        public void Update(float elapsed) => OnUpdate(elapsed);

        protected virtual void OnRender()
        {
        }

        protected virtual void OnUpdate(float elapsed)
        {
        }
    }
}