// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Extensions;
using HenHen.Framework.Worlds.Mediums;
using HenHen.Framework.Worlds.Nodes;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Worlds.Chunks
{
    /// <summary>
    /// Manages and exposes chunks.
    /// </summary>
    public class ChunksManager
    {
        public IReadOnlyDictionary<Vector2, Chunk> Chunks { get; }
        public float ChunkSize { get; }

        public ChunksManager(Vector2 chunkCount, float chunkSize)
        {
            Chunks = CreateChunks(chunkCount, chunkSize);
            ChunkSize = chunkSize;
        }

        /// <remarks>
        /// Will return an index even if
        /// there is no chunk at that index.
        /// </remarks>
        public Vector2 GetChunkIndexForPosition(Vector2 position) => new((int)(position.X / ChunkSize), (int)(position.Y / ChunkSize));

        public Chunk GetChunkForPosition(Vector2 position) => Chunks[GetChunkIndexForPosition(position)];

        public void AddMedium(Medium medium)
        {
            foreach (var chunk in GetChunksForMedium(medium))
                chunk.AddMedium(medium);
        }

        public void AddNode(Node node)
        {
            foreach (var chunk in GetChunksForNode(node))
                chunk.AddNode(node);
        }

        private static Dictionary<Vector2, Chunk> CreateChunks(Vector2 chunkCount, float chunkSize)
        {
            var chunks = new Dictionary<Vector2, Chunk>((int)(chunkCount.X * chunkCount.Y));
            for (var x = 0; x < chunkCount.X; x++)
            {
                for (var y = 0; y < chunkCount.Y; y++)
                {
                    var index = new Vector2(x, y);
                    chunks.Add(index, new Chunk(index, chunkSize));
                }
            }
            return chunks;
        }

        private static IEnumerable<Vector2> EnumerateTopDownVerticesOfMedium(Medium medium)
        {
            yield return medium.Triangle.A.ToTopDownPoint();
            yield return medium.Triangle.B.ToTopDownPoint();
            yield return medium.Triangle.C.ToTopDownPoint();
        }

        private IEnumerable<Chunk> GetChunksForNode(Node node)
        {
            if (node.CollisionBody is null)
            {
                yield return GetChunkForPosition(node.Position.ToTopDownPoint());
                yield break;
            }
            var rect = (node.CollisionBody.BoundingBox + node.Position).ToTopDownRectangle();
            var bottomLeft = GetChunkIndexForPosition(new Vector2(rect.Left, rect.Bottom));
            var topRight = GetChunkIndexForPosition(new Vector2(rect.Right, rect.Top));
            for (var x = bottomLeft.X; x <= topRight.X; x++)
            {
                for (var y = bottomLeft.Y; y <= topRight.Y; y++)
                    yield return Chunks[new Vector2(x, y)];
            }
        }

        private IEnumerable<Chunk> GetChunksForMedium(Medium medium)
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
            for (var x = (int)bottomLeftChunkIndex.X; x <= topRightChunkIndex.X; x++)
            {
                for (var y = (int)bottomLeftChunkIndex.Y; y <= topRightChunkIndex.Y; y++)
                {
                    var chunkIndex = new Vector2(x, y);
                    yield return Chunks[chunkIndex];
                }
            }
        }
    }
}