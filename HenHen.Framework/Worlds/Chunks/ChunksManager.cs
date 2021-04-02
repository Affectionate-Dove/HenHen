// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Extensions;
using HenHen.Framework.Numerics;
using HenHen.Framework.Worlds.Mediums;
using HenHen.Framework.Worlds.Nodes;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Worlds.Chunks
{
    /// <summary>
    /// Used to manage <see cref="Chunk"/>s.
    /// </summary>
    public class ChunksManager
    {
        public IReadOnlyDictionary<Vector2, Chunk> Chunks { get; }
        public float ChunkSize { get; }
        public object SynchronizedTime { get; private set; }

        public ChunkSimulationManager SimulationManager { get; }

        public ChunksManager(Vector2 chunkCount, float chunkSize)
        {
            if (chunkCount.X is <= 0 || chunkCount.Y <= 0)
                throw new ArgumentOutOfRangeException(nameof(chunkCount), "Both coordinates have to be positive.");

            var chunks = new Dictionary<Vector2, Chunk>((int)(chunkCount.X * chunkCount.Y));
            for (var x = 0; x < chunkCount.X; x++)
            {
                for (var y = 0; y < chunkCount.Y; y++)
                {
                    var chunkPosition = new Vector2(x, y);
                    chunks.Add(chunkPosition, new Chunk(chunkPosition, this));
                }
            }

            Chunks = chunks;
            ChunkSize = chunkSize;
            SimulationManager = new(this);
        }

        public void AddNode(Node node)
        {
            var targetChunk = GetChunkForPosition(node.Position.ToTopDownPoint());
            targetChunk.AddNode(node);
        }

        public void AddMedium(Medium medium)
        {
            foreach (var chunk in GetChunksForMedium(medium))
                chunk.AddMedium(medium);
        }

        public Chunk GetChunkForPosition(Vector2 position) => Chunks[GetChunkIndexForPosition(position)];

        public IEnumerable<Chunk> GetChunks(RectangleF rectangle)
        {
            for (var x = rectangle.Left; x <= rectangle.Right; x += ChunkSize)
            {
                for (var y = rectangle.Bottom; y <= rectangle.Top; y += ChunkSize)
                    yield return GetChunkForPosition(new Vector2(x, y));
            }
        }

        protected Vector2 GetChunkIndexForPosition(Vector2 position)
        {
            var chunkIndex = position / ChunkSize;
            return new Vector2((int)chunkIndex.X, (int)chunkIndex.Y);
        }

        protected IEnumerable<Chunk> GetChunksForMedium(Medium medium)
        {
            var vertexEnumerator = EnumerateTopDownVerticesOfMedium(medium).GetEnumerator();
            vertexEnumerator.MoveNext();
            var vertex = vertexEnumerator.Current;
            var mostLeftPos = vertex.X;
            var mostRightPos = vertex.X;
            var mostUpPos = vertex.Y;
            var mostDownPos = vertex.Y;
            while (vertexEnumerator.MoveNext())
            {
                mostLeftPos = Math.Min(mostLeftPos, vertexEnumerator.Current.X);
                mostDownPos = Math.Min(mostDownPos, vertexEnumerator.Current.Y);
                mostRightPos = Math.Max(mostRightPos, vertexEnumerator.Current.X);
                mostUpPos = Math.Max(mostUpPos, vertexEnumerator.Current.Y);
            }
            var bottomLeftChunkIndex = GetChunkIndexForPosition(new Vector2(mostLeftPos, mostDownPos));
            var topRightChunkIndex = GetChunkIndexForPosition(new Vector2(mostRightPos, mostUpPos));
            for (var x = (int)bottomLeftChunkIndex.X; x < topRightChunkIndex.X; x++)
            {
                for (var y = (int)bottomLeftChunkIndex.Y; y < topRightChunkIndex.Y; y++)
                {
                    var chunkIndex = new Vector2(x, y);
                    yield return Chunks[chunkIndex];
                }
            }
        }

        private static IEnumerable<Vector2> EnumerateTopDownVerticesOfMedium(Medium medium)
        {
            yield return medium.Triangle.A.ToTopDownPoint();
            yield return medium.Triangle.B.ToTopDownPoint();
            yield return medium.Triangle.C.ToTopDownPoint();
        }
    }
}