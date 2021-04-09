﻿// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;
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

        public IEnumerable<Chunk> GetChunksForRectangle(RectangleF rectangle)
        {
            var rightIndex = rectangle.Right / ChunkSize;
            var topIndex = rectangle.Top / ChunkSize;
            for (var x = (int)(rectangle.Left / ChunkSize); x <= rightIndex; x++)
            {
                for (var y = (int)(rectangle.Bottom / ChunkSize); y <= topIndex; y++)
                    yield return Chunks[new Vector2(x, y)];
            }
        }

        public IEnumerable<Chunk> GetChunksForCircle(Circle circle)
        {
            var rect = new RectangleF(circle.CenterPosition.X - circle.Radius, circle.CenterPosition.X + circle.Radius, circle.CenterPosition.Y - circle.Radius, circle.CenterPosition.Y + circle.Radius);
            foreach (var chunk in GetChunksForRectangle(rect))
            {
                if (ElementaryCollisions.IsPointInCircle(chunk.Coordinates.Center, circle))
                    yield return chunk;
            }
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
            foreach (var chunk in GetChunksForRectangle(rect))
                yield return chunk;
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

            var rect = new RectangleF(mostLeftPos, mostRightPos, mostDownPos, mostUpPos);
            foreach (var chunk in GetChunksForRectangle(rect))
                yield return chunk;
        }
    }
}