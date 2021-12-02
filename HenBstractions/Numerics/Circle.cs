// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenBstractions.Numerics
{
    public struct Circle
    {
        public Vector2 CenterPosition { get; set; }
        public float Radius { get; set; }

        public float Diameter
        {
            get => 2 * Radius;
            set => Radius = value / 2;
        }

        public float Circumference => 2 * System.MathF.PI * Radius;

        public float Area => System.MathF.PI * (Radius * Radius);

        public Circle(Vector2 centerPosition, float radius)
        {
            CenterPosition = centerPosition;
            Radius = radius;
        }

        public static Circle operator +(Circle circle, Vector2 v) => new()
        {
            CenterPosition = circle.CenterPosition + v
        };

        public static Circle operator -(Circle circle, Vector2 v) => new()
        {
            CenterPosition = circle.CenterPosition - v
        };

        public static Circle operator *(Circle circle, Vector2 v) => new()
        {
            CenterPosition = circle.CenterPosition * v
        };

        public static Circle operator /(Circle circle, Vector2 v) => new()
        {
            CenterPosition = circle.CenterPosition / v
        };

        public override string ToString() => $"{{{nameof(CenterPosition)}={CenterPosition},{nameof(Radius)}={Radius}}}";
    }
}