// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using HenHen.Framework.Worlds.Mediums;
using HenHen.Framework.Worlds.Nodes;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Worlds.Chunks
{
    public class Chunk
    {
        public Vector2 Index { get; }
        public ICollection<Medium> Mediums { get; } = new List<Medium>();
        public ICollection<Node> Nodes { get; } = new List<Node>();
        public int SynchronizedTime { get; set; }
        public RectangleF Coordinates { get; }

        public Chunk(Vector2 index, float size)
        {
            Index = index;
            var coordinates = new RectangleF
            {
                Left = index.X * size,
                Bottom = index.Y * size
            };
            coordinates.Right = coordinates.Left + size;
            coordinates.Top = coordinates.Bottom + size;
            Coordinates = coordinates;
        }
    }
}