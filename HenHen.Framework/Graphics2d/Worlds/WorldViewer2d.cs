// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Extensions;
using HenHen.Framework.Worlds;
using HenHen.Framework.Worlds.Chunks;
using HenHen.Framework.Worlds.Mediums;
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

        public Func<GridLineInfo, GridLineRenderConfig?> GetGridLineRenderConfig { get; set; } = DefaultGetGridLineRenderConfig;

        public Func<Chunk, FillBorderColorsConfig?> GetChunkRenderConfig { get; set; } = DefaultGetChunkRenderConfig;

        public Func<Node, NodeRenderConfig?> GetNodeRenderConfig { get; set; } = DefaultGetNodeRenderConfig;

        public Func<Medium, FillBorderColorsConfig?> GetMediumRenderConfig { get; set; } = DefaultGetMediumRenderConfig;

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

        public static FillBorderColorsConfig? DefaultGetChunkRenderConfig(Chunk chunk) => new()
        {
            BorderColor = new Raylib_cs.Color(255, 255, 255, 100),
            FillColor = new Raylib_cs.Color(30, 30, 30, 255)
        };

        public static GridLineRenderConfig? DefaultGetGridLineRenderConfig(GridLineInfo gridLineInfo) => new()
        {
            Color = new Raylib_cs.Color(255, 255, 255, 50),
            Thickness = 1
        };

        public static NodeRenderConfig? DefaultGetNodeRenderConfig(Node node) => new()
        {
            Color = Raylib_cs.Color.RAYWHITE,
            Size = 5
        };

        public static FillBorderColorsConfig? DefaultGetMediumRenderConfig(Medium medium) => new()
        {
            BorderColor = medium.Color,
            FillColor = Raylib_cs.Raylib.ColorAlpha(medium.Color, 0.04f)
        };

        protected override void OnRender()
        {
            base.OnRender();

            if (GetChunkRenderConfig is not null)
                DrawChunks();

            if (GetMediumRenderConfig is not null)
                DrawMediums();

            if (GetGridLineRenderConfig is not null)
                DrawGrid();

            if (GetNodeRenderConfig is not null)
                DrawNodes();
        }

        private void DrawMediums()
        {
            var visibleArea = camera.GetVisibleArea(LayoutInfo.RenderSize);
            foreach (var medium in World.GetMediumsAroundArea(visibleArea))
            {
                var renderConfig = GetMediumRenderConfig(medium);
                if (!renderConfig.HasValue)
                    continue;

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

                if (renderConfig.Value.BorderColor.HasValue)
                    Raylib_cs.Raylib.DrawTriangleLines(triangle2d.A, triangle2d.B, triangle2d.C, renderConfig.Value.BorderColor.Value);

                if (renderConfig.Value.FillColor.HasValue)
                    Raylib_cs.Raylib.DrawTriangle(triangle2d.A, triangle2d.B, triangle2d.C, renderConfig.Value.FillColor.Value);
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
                var lineRenderConfig = GetGridLineRenderConfig(new() { Coordinate = currentY * gridDistance, Vertical = false });
                if (!lineRenderConfig.HasValue)
                    continue;

                // Y inside the local space
                var localRenderingY = camera.PositionToRenderingSpace(new(0, currentY * gridDistance), LayoutInfo.RenderSize).Y;

                // Y on screen
                var renderingY = (int)Math.Round(LayoutInfo.RenderRect.Top + localRenderingY);

                Raylib_cs.Raylib.DrawLineEx(new(LayoutInfo.RenderRect.Left, renderingY), new(LayoutInfo.RenderRect.Right, renderingY), lineRenderConfig.Value.Thickness, lineRenderConfig.Value.Color);
            }

            var startX = MathF.Ceiling(visibleArea.Left / gridDistance);
            var stopX = MathF.Floor(visibleArea.Right / gridDistance);
            for (var currentX = startX; currentX <= stopX; currentX++)
            {
                var lineRenderConfig = GetGridLineRenderConfig(new() { Coordinate = currentX * gridDistance, Vertical = true });
                if (!lineRenderConfig.HasValue)
                    continue;

                // X inside the local space
                var localRenderingX = camera.PositionToRenderingSpace(new(currentX * gridDistance, 0), LayoutInfo.RenderSize).X;

                // X on screen
                var renderingX = (int)Math.Round(LayoutInfo.RenderRect.Left + localRenderingX);

                Raylib_cs.Raylib.DrawLineEx(new(renderingX, LayoutInfo.RenderRect.Bottom), new(renderingX, LayoutInfo.RenderRect.Top), lineRenderConfig.Value.Thickness, lineRenderConfig.Value.Color);
            }
        }

        private void DrawNodes()
        {
            var visibleArea = camera.GetVisibleArea(LayoutInfo.RenderSize);
            foreach (var node in World.GetNodesAroundArea(visibleArea))
            {
                var renderConfig = GetNodeRenderConfig(node);
                if (!renderConfig.HasValue)
                    continue;

                var localRendering = camera.PositionToRenderingSpace(node.Position.ToTopDownPoint(), LayoutInfo.RenderSize);

                var renderingPos = LayoutInfo.RenderRect.TopLeft + localRendering;

                var size = new Vector2(renderConfig.Value.Size);
                Raylib_cs.Raylib.DrawRectangleV(renderingPos - (size * 0.5f), size, renderConfig.Value.Color);
            }
        }

        private void DrawChunks()
        {
            var visibleArea = camera.GetVisibleArea(LayoutInfo.RenderSize);
            foreach (var chunk in World.GetChunksAroundArea(visibleArea))
            {
                var renderConfig = GetChunkRenderConfig(chunk);
                if (!renderConfig.HasValue)
                    continue;

                var localRenderingArea = camera.AreaToRenderingSpace(chunk.Coordinates, LayoutInfo.RenderSize);
                var screenRenderingArea = localRenderingArea + LayoutInfo.RenderRect.TopLeft;

                if (renderConfig.Value.FillColor.HasValue)
                    Raylib_cs.Raylib.DrawRectangleV(screenRenderingArea.TopLeft, screenRenderingArea.Size, renderConfig.Value.FillColor.Value);

                if (renderConfig.Value.BorderColor.HasValue)
                    Raylib_cs.Raylib.DrawRectangleLines((int)screenRenderingArea.Left, (int)screenRenderingArea.Top, (int)screenRenderingArea.Width + 1, (int)screenRenderingArea.Height + 1, renderConfig.Value.BorderColor.Value);
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

        public readonly struct GridLineRenderConfig
        {
            private readonly int thickness;

            public int Thickness
            {
                get => thickness;
                init
                {
                    if (value is <= 0)
                        throw new Exception("The thickness of a line can't be less than or equal to 0.");

                    thickness = value;
                }
            }

            public ColorInfo Color { get; init; }
        }

        public readonly struct FillBorderColorsConfig
        {
            public ColorInfo? FillColor { get; init; }
            public ColorInfo? BorderColor { get; init; }

            public FillBorderColorsConfig(ColorInfo? fillColor, ColorInfo? borderColor)
            {
                FillColor = fillColor;
                BorderColor = borderColor;
            }
        }

        public readonly struct NodeRenderConfig
        {
            private readonly float size;

            public ColorInfo Color { get; init; }

            public float Size
            {
                get => size;
                init
                {
                    if (value is <= 0)
                        throw new Exception("The size of a node can't be less than or equal to 0.");

                    size = value;
                }
            }
        }
    }
}