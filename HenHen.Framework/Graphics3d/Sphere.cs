using HenHen.Framework.Graphics2d;
using System.Numerics;

namespace HenHen.Framework.Graphics3d
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

        public Circle ToTopDownCircle() => new Circle
            {
                CenterPosition = new Vector2(CenterPosition.X, CenterPosition.Z),
                Radius = Radius
            };
    }
}