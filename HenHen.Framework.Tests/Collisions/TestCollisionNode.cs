﻿// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;
using HenHen.Framework.Numerics;
using HenHen.Framework.Worlds.Nodes;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Tests.Collisions
{
    public class TestCollisionNode : Node
    {
        public List<Node> CollisionRecord { get; } = new();

        public TestCollisionNode(int id, Vector3 position, IEnumerable<Sphere> spheres)
        {
            Id = id;
            Position = position;
            CollisionBody = new CollisionBody(spheres);
        }

        public override void OnCollision(Node other)
        {
            base.OnCollision(other);
            CollisionRecord.Add(other);
        }
    }
}