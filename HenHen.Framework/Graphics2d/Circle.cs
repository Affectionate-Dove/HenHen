using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public struct Circle
    {
        public Vector2 CenterPosition;
        public float Radius;

        public float Diameter
        {
            get => 2 * Radius;
            set => Radius = value/2;
        }
        public float Circumference => 2 * System.Math.PI * Radius;
        public float Area => System.Math.PI * (Radius * Radius);
    }
}