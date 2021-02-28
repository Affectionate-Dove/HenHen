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
            set => Radius = value;
        }
        public float Circumference
        {
            get => 2 * System.Math.PI * Radius;
        }
        public float Area
        {
            get => System.Math.PI * (Radius * Radius);
        }
    }