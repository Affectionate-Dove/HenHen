// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Extensions;
using HenHen.Framework.Worlds;
using HenHen.Framework.Worlds.Mediums;
using System;
using System.Numerics;

namespace HenHen.Framework.Graphics2d.Worlds
{
    public class WorldViewer2d : Container
    {
        private readonly Camera2D camera = new();
        private float gridDistance = 2;

        public World World { get; }

        /// <summary>
        ///     Defines the point displayed
        ///     at the center of this <see cref="WorldViewer2d"/>.
        /// </summary>
        public Vector2 Target
        {
            get => camera.Target;
            set => camera.Target = value;
        }

        /// <summary>
        ///     How many square units are there in each square
        ///     defined by grid lines.
        /// </summary>
        /// <remarks>
        ///     Values less than 0 are automatically rounded up to 0.
        ///     If this is set to 0, grid will not be drawn.
        /// </remarks>
        public float GridDistance
        {
            get => gridDistance;
            set => gridDistance = Math.Max(0, value);
        }

        public WorldViewer2d(World world)
        {
            camera.FovY = world.Size.Y;
            AddChild(new Rectangle
            {
                Color = new ColorInfo(0, 0, 0),
                RelativeSizeAxes = Axes.Both
            });
            World = world;
            Masking = true;
        }

        protected override void OnRender()
        {
            base.OnRender();
            DrawChunksFill();
            DrawChunksBorders();
            DrawMediums();
            DrawGrid();
            DrawNodes();
        }

        private static ColorInfo GetMediumColor(MediumType type) => type switch
        {
            MediumType.Ground => new(0, 255, 0),
            MediumType.Water => new(0, 100, 255),
            MediumType.Air => new(255, 255, 255, 50),
            _ => new(0, 0, 0, 50)
        };

        private void DrawMediums()
        {
            var visibleArea = camera.GetVisibleArea(LayoutInfo.RenderSize);
            foreach (var medium in World.GetMediumsAroundArea(visibleArea))
            {
                // convert 3d triangle to 2d triangle
                var triangle2d = medium.Triangle.ToTopDownTriangle();

                // this position is what would be returned as (0, 0):
                var zeroPixelsInWorld = visibleArea.TopLeft;

                // make the triangle be in a position
                // relative to the top left corner of visible area
                triangle2d -= zeroPixelsInWorld;

                // convert the triangle from world units to pixels
                triangle2d *= Camera2D.GetPixelsPerUnit(LayoutInfo.RenderSize, visibleArea.Width);

                // move the triangle into the space of this container
                triangle2d += LayoutInfo.RenderRect.TopLeft;

                // calls to drawing framework
                var borderColor = GetMediumColor(medium.Type).ToRaylibColor();
                Raylib_cs.Raylib.DrawTriangleLines(triangle2d.A, triangle2d.B, triangle2d.C, borderColor);
                var fillColor = new ColorInfo(borderColor.r, borderColor.g, borderColor.b, 10).ToRaylibColor();
                Raylib_cs.Raylib.DrawTriangle(triangle2d.A, triangle2d.B, triangle2d.C, fillColor);
            }
        }

        private void DrawGrid()
        {
            if (GridDistance == 0)
                return;

            var visibleArea = camera.GetVisibleArea(LayoutInfo.RenderSize);

            var startY = MathF.Ceiling(visibleArea.Bottom / gridDistance);
            var stopY = MathF.Floor(visibleArea.Top / gridDistance);
            for (var currentY = startY; currentY <= stopY; currentY++)
            {
                // Y inside the local space
                var localRenderingY = camera.PositionToRenderingSpace(new(0, currentY * gridDistance), LayoutInfo.RenderSize).Y;

                // Y on screen
                var renderingY = (int)Math.Round(LayoutInfo.RenderRect.Top + localRenderingY);

                Raylib_cs.Raylib.DrawLine((int)LayoutInfo.RenderRect.Left, renderingY, (int)LayoutInfo.RenderRect.Right, renderingY, new Raylib_cs.Color(255, 255, 255, 50));
            }

            var startX = MathF.Ceiling(visibleArea.Left / gridDistance);
            var stopX = MathF.Floor(visibleArea.Right / gridDistance);
            for (var currentX = startX; currentX <= stopX; currentX++)
            {
                // X inside the local space
                var localRenderingX = camera.PositionToRenderingSpace(new(currentX * gridDistance, 0), LayoutInfo.RenderSize).X;

                // X on screen
                var renderingX = (int)Math.Round(LayoutInfo.RenderRect.Left + localRenderingX);

                Raylib_cs.Raylib.DrawLine(renderingX, (int)LayoutInfo.RenderRect.Top, renderingX, (int)LayoutInfo.RenderRect.Bottom, new Raylib_cs.Color(255, 255, 255, 50));
            }
        }

        private void DrawNodes()
        {
            var visibleArea = camera.GetVisibleArea(LayoutInfo.RenderSize);
            foreach (var node in World.GetNodesAroundArea(visibleArea))
            {
                var localRendering = camera.PositionToRenderingSpace(node.Position.ToTopDownPoint(), LayoutInfo.RenderSize);

                var rendering = LayoutInfo.RenderRect.TopLeft + localRendering;

                Raylib_cs.Raylib.DrawRectangleV(rendering, new Vector2(5), new Raylib_cs.Color(255, 255, 255, 255));
            }
        }

        private void DrawChunksFill()
        {
            var visibleArea = camera.GetVisibleArea(LayoutInfo.RenderSize);
            foreach (var chunk in World.GetChunksAroundArea(visibleArea))
            {
                var localRenderingArea = camera.AreaToRenderingSpace(chunk.Coordinates, LayoutInfo.RenderSize);
                var screenRenderingArea = localRenderingArea + LayoutInfo.RenderRect.TopLeft;
                Raylib_cs.Raylib.DrawRectangleV(screenRenderingArea.TopLeft, screenRenderingArea.Size, new Raylib_cs.Color(30, 30, 30, 255));
            }
        }

        private void DrawChunksBorders()
        {
            var visibleArea = camera.GetVisibleArea(LayoutInfo.RenderSize);
            foreach (var chunk in World.GetChunksAroundArea(visibleArea))
            {
                var localRenderingArea = camera.AreaToRenderingSpace(chunk.Coordinates, LayoutInfo.RenderSize);
                var screenRenderingArea = localRenderingArea + LayoutInfo.RenderRect.TopLeft;
                Raylib_cs.Raylib.DrawRectangleLines((int)screenRenderingArea.Left, (int)screenRenderingArea.Top, (int)screenRenderingArea.Width + 1, (int)screenRenderingArea.Height + 1, new Raylib_cs.Color(255, 255, 255, 100));
            }
        }
    }
}