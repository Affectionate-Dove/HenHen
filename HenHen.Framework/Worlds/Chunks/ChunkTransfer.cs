// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Worlds.Nodes;

namespace HenHen.Framework.Worlds.Chunks
{
    public struct ChunkTransfer
    {
        public Node Node { get; init; }
        public Chunk From { get; init; }
        public Chunk To { get; init; }
    }
}