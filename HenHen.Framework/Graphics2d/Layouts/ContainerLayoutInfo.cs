﻿// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;

namespace HenHen.Framework.Graphics2d.Layouts
{
    public readonly struct ContainerLayoutInfo
    {
        /// <summary>
        ///     The area at the screen that is allowed
        ///     to render children, in pixels.
        /// </summary>
        /// <remarks>
        ///     If null, children should not be rendered.
        /// </remarks>
        public RectangleF? MaskArea { get; init; }

        /// <summary>
        ///     The boundaries of the space where this container's
        ///     children can be on screen, in pixels.
        /// </summary>
        public RectangleF ChildrenRenderArea { get; init; }
    }
}