// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenHen.Framework.Numerics
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

        public Triangle2 ToTopDownTriangle() => new()
        {
            A = new Vector2(A.X, A.Z),
            B = new Vector2(B.X, B.Z),
            C = new Vector2(C.X, C.Z)
        };

        public override string ToString() => $"{{{nameof(A)}={A},{nameof(B)}={B},{nameof(C)}={C}}}";
    }
}