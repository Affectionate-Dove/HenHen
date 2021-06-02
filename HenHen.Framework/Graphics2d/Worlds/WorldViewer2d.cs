// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Extensions;
using HenHen.Framework.Worlds;
using HenHen.Framework.Worlds.Mediums;

namespace HenHen.Framework.Graphics2d.Worlds
{
    public class WorldViewer2d : Container
    {
        private readonly Camera2D camera = new();
        public World World { get; }

        public WorldViewer2d(World world)
        {
            camera.FovY = world.Size.Y;
            AddChild(new Rectangle
            {
                Color = new ColorInfo(20, 20, 20),
                RelativeSizeAxes = Axes.Both
            });
            World = world;
        }

        protected override void OnRender()
        {
            base.OnRender();
            DrawMediums();
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
    }
}