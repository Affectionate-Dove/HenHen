// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using System.Numerics;

namespace HenHen.Framework.Graphics2d.Worlds
{
    public class Camera2D
    {
        /// <summary>
        ///     The point the camera is looking at.
        /// </summary>
        public Vector2 Target { get; set; }

        /// <summary>
        ///     The amount of visible vertical space.
        /// </summary>
        public float FovY { get; set; }

        /// <summary>
        ///     Returns a rectangle that defines the area
        ///     of the world that is visible (in world units).
        /// </summary>
        /// <remarks>
        ///     If either coordinate of <paramref name="renderingSpaceSize"/>
        ///     is less than or equal to 0, returns default rectangle.
        /// </remarks>
        public RectangleF GetVisibleArea(Vector2 renderingSpaceSize)
        {
            var size = GetVisibleAreaSize(renderingSpaceSize);
            if (size == Vector2.Zero)
                return new();
            var halfHeight = size.Y * 0.5f;
            var halfWidth = size.X * 0.5f;
            return new(Target.X - halfWidth, Target.X + halfWidth, Target.Y - halfHeight, Target.Y + halfHeight);
        }

        public Vector2 GetVisibleAreaSize(Vector2 renderingSpaceSize)
        {
            if (renderingSpaceSize.X is <= 0 || renderingSpaceSize.Y is <= 0)
                return new();
            var height = FovY;
            var widthToHeightRatio = renderingSpaceSize.X / renderingSpaceSize.Y;
            var width = widthToHeightRatio * height;
            return new(width, height);
        }

        /// <summary>
        ///     Translates a position in world (world units)
        ///     to a position in rendering area (pixels).
        /// </summary>
        public Vector2 PositionToRenderingSpace(Vector2 positionInWorld, Vector2 renderingSpaceSize)
        {
            var visibleArea = GetVisibleArea(renderingSpaceSize);

            Vector2 pixelsPerUnit;
            {
                var pixelsPerUnitF = renderingSpaceSize.X / visibleArea.Width;
                pixelsPerUnit = new Vector2(pixelsPerUnitF, -pixelsPerUnitF);
                // since world has Y going up, and rendering has Y going down,
                // Y is with a minus
            }

            // this position is what would be returned as (0, 0):
            var zeroPixelsInWorld = visibleArea.TopLeft;

            var positionInWorldRelativeToZeroPixelsInWorld = positionInWorld - zeroPixelsInWorld;
            var positionInPixels = positionInWorldRelativeToZeroPixelsInWorld * pixelsPerUnit;

            return positionInPixels;
        }

        /// <summary>
        ///     Translates an area rectangle in world (world units)
        ///     to an area rectangle in rendering area (pixels).
        /// </summary>
        public RectangleF AreaToRenderingSpace(RectangleF area, Vector2 renderingSpaceSize)
        {
            var visibleArea = GetVisibleArea(renderingSpaceSize);

            Vector2 pixelsPerUnit;
            {
                var pixelsPerUnitF = renderingSpaceSize.X / visibleArea.Width;
                pixelsPerUnit = new Vector2(pixelsPerUnitF, -pixelsPerUnitF);
                // since world has Y going up, and rendering has Y going down,
                // Y is with a minus
            }

            // this position is what would be returned as (0, 0):
            var zeroPixelsInWorld = visibleArea.TopLeft;

            var areaInWorldRelativeToZeroPixelsInWorld = area - zeroPixelsInWorld;
            var areaInPixels = areaInWorldRelativeToZeroPixelsInWorld * pixelsPerUnit;

            return areaInPixels;
        }
    }
}