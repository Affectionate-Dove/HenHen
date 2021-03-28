// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;

namespace HenHen.Core.Hens
{
    /// <summary>
    /// Describes a kind ("breed") of a Hen.
    /// </summary>
    public record HenKind
    {
        public string Name { get; init; }
        public CollisionBody CollisionBody { get; init; }
    }
}