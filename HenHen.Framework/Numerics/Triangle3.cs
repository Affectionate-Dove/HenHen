// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenHen.Framework.Numerics
{
    /// <summary>
    ///     Triangle in 3D space.
    /// </summary>
    public readonly struct Triangle3
    {
        public Vector3 A { get; }
        public Vector3 B { get; }
        public Vector3 C { get; }

        /// <summary>
        ///     Mean position of all vertices.
        /// </summary>
        public Vector3 Centroid
        {
            get
            {
                var centerOfEdgeBC = (B + C) * 0.5f;
                var median = centerOfEdgeBC - A;
                return A + (median * 2 / 3f);
            }
        }

        /// <summary>
        ///     The direction that the face of this
        ///     <see cref="Triangle3"/> is pointing in.
        /// </summary>
        public Vector3 Normal => Vector3.Cross(A, B);

        public Triangle3(Vector3 a, Vector3 b, Vector3 c)
        {
            A = a;
            B = b;
            C = c;
        }

        public static Triangle3 operator +(Triangle3 triangle3, Vector3 v) => new(triangle3.A + v, triangle3.B + v, triangle3.C + v);

        public static Triangle3 operator -(Triangle3 triangle3, Vector3 v) => new(triangle3.A - v, triangle3.B - v, triangle3.C - v);

        public static Triangle3 operator *(Triangle3 triangle3, Vector3 v) => new(triangle3.A * v, triangle3.B * v, triangle3.C * v);

        public static Triangle3 operator /(Triangle3 triangle3, Vector3 v) => new(triangle3.A / v, triangle3.B / v, triangle3.C / v);

        public Triangle2 ToTopDownTriangle() => new(new Vector2(A.X, A.Z), new Vector2(B.X, B.Z), new Vector2(C.X, C.Z));

        /// <summary>
        ///     Creates a <see cref="Triangle3"/> with <see cref="C"/>
        ///     and <see cref="A"/> vertices swapped.
        /// </summary>
        public Triangle3 Reversed() => new(C, B, A);

        public override string ToString() => $"{{{nameof(A)}={A},{nameof(B)}={B},{nameof(C)}={C}}}";
    }
}