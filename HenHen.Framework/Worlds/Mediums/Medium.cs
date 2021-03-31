// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;

namespace HenHen.Framework.Worlds.Mediums
{
    public class Medium
    {
        public MediumType Type { get; set; }
        public Triangle3 Triangle { get; set; }

        public override string ToString() => $"{{{nameof(Type)}={Type},{nameof(Triangle)}={Triangle}}}";
    }

    public enum MediumType
    {
        Ground,
        Water,
        Air
    }
}