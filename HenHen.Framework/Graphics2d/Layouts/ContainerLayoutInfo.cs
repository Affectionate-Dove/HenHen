// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using System.Numerics;

namespace HenHen.Framework.Graphics2d.Layouts
{
    public readonly struct ContainerLayoutInfo
    {
        public Vector2 ChildrenRenderPosition { get; init; }
        public Vector2 ChildrenRenderSize { get; init; }

        /// <summary>
        ///     The area at the screen that is allowed
        ///     to render children.
        /// </summary>
        /// <remarks>
        ///     If null, children should not be rendered.
        /// </remarks>
        public RectangleF? MaskArea { get; init; }

        public RectangleF ChildrenRenderArea => new()
        {
            TopLeft = ChildrenRenderPosition,
            Size = ChildrenRenderSize
        };
    }
}