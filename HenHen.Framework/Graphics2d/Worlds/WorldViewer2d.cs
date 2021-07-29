// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Extensions;
using HenHen.Framework.Worlds;
using HenHen.Framework.Worlds.Chunks;
using HenHen.Framework.Worlds.Nodes;
using System;
using System.Numerics;

namespace HenHen.Framework.Graphics2d.Worlds
{
    public class WorldViewer2d : Container
    {
        private readonly Camera2D camera = new();
        private readonly Rectangle background;
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

        public ColorInfo BackgroundColor
        {
            get => background.Color;
            set => background.Color = value;
        }

        public Func<GridLineInfo, ColorInfo> GetGridLineColor { get; set; } = DefaultGetGridColor;

        public Func<Chunk, ColorInfo> GetChunkBorderColor { get; set; } = DefaultGetChunkBorderColor;

        public Func<Chunk, ColorInfo> GetChunkFillColor { get; set; } = DefaultGetChunkFillColor;

        public Func<Node, ColorInfo> GetNodeColor { get; set; } = DefaultGetNodeColor;

        public Func<Node, float> GetNodeSize { get; set; } = DefaultGetNodeSize;

        /// <summary>
        ///     Amount of visible vertical space.
        /// </summary>
        public float FieldOfView
        {
            get => camera.FovY;
            set => camera.FovY = value;
        }

        public WorldViewer2d(World world)
        {
            Target = world.Size / 2;
            FieldOfView = world.Size.Y;
            AddChild(background = new Rectangle
            {
                Color = new ColorInfo(0, 0, 0),
                RelativeSizeAxes = Axes.Both
            });
            World = world;
            Masking = true;
        }

        public static ColorInfo DefaultGetChunkBorderColor(Chunk chunk) => new Raylib_cs.Color(255, 255, 255, 100);

        public static ColorInfo DefaultGetChunkFillColor(Chunk chunk) => new Raylib_cs.Color(30, 30, 30, 255);

        public static float DefaultGetNodeSize(Node node) => 5;

        public static ColorInfo DefaultGetGridColor(GridLineInfo gridLineInfo) => new Raylib_cs.Color(255, 255, 255, 50);

        public static ColorInfo DefaultGetNodeColor(Node node) => Raylib_cs.Color.RAYWHITE;

        protected override void OnRender()
        {
            base.OnRender();
            if (GetChunkFillColor is not null)
                DrawChunksFill(GetChunkFillColor);
            if (GetChunkBorderColor is not null)
                DrawChunksBorders();
            DrawMediums();
            if (GetGridLineColor is not null)
                DrawGrid();
            if (GetNodeColor is not null && GetNodeSize is not null)
                DrawNodes();
        }

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
                var borderColor = medium.Color;
                Raylib_cs.Raylib.DrawTriangleLines(triangle2d.A, triangle2d.B, triangle2d.C, borderColor);
                var fillColor = new ColorInfo(borderColor.r, borderColor.g, borderColor.b, 10);
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

                var lineColor = GetGridLineColor(new() { Coordinate = currentY * gridDistance, Vertical = false });

                Raylib_cs.Raylib.DrawLine((int)LayoutInfo.RenderRect.Left, renderingY, (int)LayoutInfo.RenderRect.Right, renderingY, lineColor);
            }

            var startX = MathF.Ceiling(visibleArea.Left / gridDistance);
            var stopX = MathF.Floor(visibleArea.Right / gridDistance);
            for (var currentX = startX; currentX <= stopX; currentX++)
            {
                // X inside the local space
                var localRenderingX = camera.PositionToRenderingSpace(new(currentX * gridDistance, 0), LayoutInfo.RenderSize).X;

                // X on screen
                var renderingX = (int)Math.Round(LayoutInfo.RenderRect.Left + localRenderingX);

                var lineColor = GetGridLineColor(new() { Coordinate = currentX * gridDistance, Vertical = true });

                Raylib_cs.Raylib.DrawLine(renderingX, (int)LayoutInfo.RenderRect.Top, renderingX, (int)LayoutInfo.RenderRect.Bottom, lineColor);
            }
        }

        private void DrawNodes()
        {
            var visibleArea = camera.GetVisibleArea(LayoutInfo.RenderSize);
            foreach (var node in World.GetNodesAroundArea(visibleArea))
            {
                var localRendering = camera.PositionToRenderingSpace(node.Position.ToTopDownPoint(), LayoutInfo.RenderSize);

                var renderingPos = LayoutInfo.RenderRect.TopLeft + localRendering;

                var size1d = GetNodeSize(node);
                if (size1d < 0)
                    throw new ArgumentOutOfRangeException(nameof(GetNodeSize), "Cannot be less than 0.");

                var size = new Vector2(size1d);
                Raylib_cs.Raylib.DrawRectangleV(renderingPos - (size * 0.5f), size, GetNodeColor(node));
            }
        }

        private void DrawChunksFill(Func<Chunk, ColorInfo> chunkFillColor)
        {
            var visibleArea = camera.GetVisibleArea(LayoutInfo.RenderSize);
            foreach (var chunk in World.GetChunksAroundArea(visibleArea))
            {
                var localRenderingArea = camera.AreaToRenderingSpace(chunk.Coordinates, LayoutInfo.RenderSize);
                var screenRenderingArea = localRenderingArea + LayoutInfo.RenderRect.TopLeft;
                Raylib_cs.Raylib.DrawRectangleV(screenRenderingArea.TopLeft, screenRenderingArea.Size, chunkFillColor(chunk));
            }
        }

        private void DrawChunksBorders()
        {
            var visibleArea = camera.GetVisibleArea(LayoutInfo.RenderSize);
            foreach (var chunk in World.GetChunksAroundArea(visibleArea))
            {
                var localRenderingArea = camera.AreaToRenderingSpace(chunk.Coordinates, LayoutInfo.RenderSize);
                var screenRenderingArea = localRenderingArea + LayoutInfo.RenderRect.TopLeft;
                Raylib_cs.Raylib.DrawRectangleLines((int)screenRenderingArea.Left, (int)screenRenderingArea.Top, (int)screenRenderingArea.Width + 1, (int)screenRenderingArea.Height + 1, GetChunkBorderColor(chunk));
            }
        }

        public readonly struct GridLineInfo
        {
            /// <summary>
            ///     Whether the line is vertical.
            /// </summary>
            public bool Vertical { get; init; }

            /// <summary>
            ///     Whether the line is horizontal.
            /// </summary>
            public bool Horizontal => !Vertical;

            public float Coordinate { get; init; }
        }
    }
}