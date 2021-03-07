// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenHen.Framework.Graphics2d.Layouts
{
    public struct ContainerLayoutInfo
    {
        public Vector2 ChildrenRenderPosition { get; init; }
        public Vector2 ChildrenRenderSize { get; init; }
    }
}