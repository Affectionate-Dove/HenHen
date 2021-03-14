// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Worlds.Nodes;

namespace HenHen.Framework.Collisions
{
    public interface ICollisionHandler
    {
        void OnCollision(Node a, Node b);
    }
}