// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenHen.Framework.Numerics
{
    public struct Triangle2
    {
        public Vector2 A;
        public Vector2 B;
        public Vector2 C;

        public Vector2 Centroid
        {
            get
            {
                var centerOfEdgeBC = (B + C) * 0.5f;
                var median = centerOfEdgeBC - A;
                return A + (median * 2 / 3f);
            }
        }

        public override string ToString() => $"{{{nameof(A)}={A},{nameof(B)}={B},{nameof(C)}={C}}}";
    }
}