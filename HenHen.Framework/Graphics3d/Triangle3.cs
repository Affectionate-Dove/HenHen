using System.Numerics;

namespace HenHen.Framework.Graphics3d
{
    public struct Triangle3
    {
        public Vector3 A;
        public Vector3 B;
        public Vector3 C;

        public Vector3 Centroid
        {
            get
            {
                var centerOfEdgeBC = (B + C) * 0.5f;
                var median = centerOfEdgeBC - A;
                return A + (median * 2 / 3f);
            }
        }

        public Vector3 Normal => Vector3.Cross(A, B);
    }
}
