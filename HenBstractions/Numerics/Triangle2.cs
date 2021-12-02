// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenBstractions.Numerics
{
    /// <summary>
    ///     Triangle in 2D space.
    /// </summary>
    public readonly struct Triangle2
    {
        public Vector2 A { get; }
        public Vector2 B { get; }
        public Vector2 C { get; }

        /// <summary>
        ///     Mean position of all vertices.
        /// </summary>
        public Vector2 Centroid
        {
            get
            {
                var centerOfEdgeBC = (B + C) * 0.5f;
                var median = centerOfEdgeBC - A;
                return A + (median * 2 / 3f);
            }
        }

        public Triangle2(in Vector2 a, in Vector2 b, in Vector2 c)
        {
            A = a;
            B = b;
            C = c;
        }

        public static Triangle2 operator +(in Triangle2 triangle2, in Vector2 v) => new(triangle2.A + v, triangle2.B + v, triangle2.C + v);

        public static Triangle2 operator -(in Triangle2 triangle2, in Vector2 v) => new(triangle2.A - v, triangle2.B - v, triangle2.C - v);

        public static Triangle2 operator *(in Triangle2 triangle2, in Vector2 v) => new(triangle2.A * v, triangle2.B * v, triangle2.C * v);

        public static Triangle2 operator /(in Triangle2 triangle2, in Vector2 v) => new(triangle2.A / v, triangle2.B / v, triangle2.C / v);

        public override string ToString() => $"{{{nameof(A)}={A},{nameof(B)}={B},{nameof(C)}={C}}}";

        /// <summary>
        ///     Creates a <see cref="Triangle2"/> with <see cref="C"/>
        ///     and <see cref="A"/> vertices swapped.
        /// </summary>
        public Triangle2 Reversed() => new(C, B, A);
    }
}