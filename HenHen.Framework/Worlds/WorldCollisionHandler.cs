// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;
using HenHen.Framework.Worlds.Nodes;

namespace HenHen.Framework.Worlds
{
    public class WorldCollisionHandler : ICollisionHandler
    {
        public void OnCollision(Node a, Node b)
        {
            a.OnCollision(b);
            b.OnCollision(a);
        }
    }
}