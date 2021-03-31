// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenHen.Framework.Numerics
{
    public struct Sphere
    {
        public Vector3 CenterPosition;
        public float Radius;
        public float SurfaceArea => 4 * System.MathF.PI * Radius * Radius;
        public float Volume => 4 / 3f * System.MathF.PI * Radius * Radius * Radius;

        public float Diameter
        {
            get => 2 * Radius;
            set => Radius = value / 2;
        }

        public Circle ToTopDownCircle() => new()
        {
            CenterPosition = new Vector2(CenterPosition.X, CenterPosition.Z),
            Radius = Radius
        };

        public override string ToString() => $"{{{nameof(CenterPosition)}={CenterPosition},{nameof(Radius)}={Radius}}}";
    }
}