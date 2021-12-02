// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Graphics;
using System.Numerics;

namespace HenFwork.Graphics3d
{
    public class ModelSpatial : Spatial
    {
        public Model Model { get; set; }

        public Vector3 Scale { get; set; } = Vector3.One;

        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);

        protected override void OnRender()
        {
            base.OnRender();
            var (axis, angle) = Rotations3d.EulerToAxisAngle(Rotation);
            Drawing.DrawModel(Model, Position, axis, angle, Scale, Color);
        }
    }
}