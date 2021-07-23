// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;

namespace HenHen.Framework.Graphics3d
{
    public class SceneViewer : Drawable
    {
        private Raylib_cs.RenderTexture2D? renderTexture;
        public Scene Scene { get; }
        public Camera Camera { get; }

        public SceneViewer(Scene scene)
        {
            Camera = new();
            Scene = scene;
        }

        protected override void OnLayoutUpdate()
        {
            base.OnLayoutUpdate();

            if (renderTexture is not null)
                Raylib_cs.Raylib.UnloadRenderTexture(renderTexture.Value);

            renderTexture = Raylib_cs.Raylib.LoadRenderTexture((int)LayoutInfo.RenderSize.X, (int)LayoutInfo.RenderSize.Y);
        }

        protected override void OnRender()
        {
            Raylib_cs.Raylib.BeginTextureMode(renderTexture.Value);
            Raylib_cs.Raylib.ClearBackground(Raylib_cs.Color.BROWN);

            Camera.Update();
            Raylib_cs.Raylib.BeginMode3D(Camera.RaylibCamera);

            foreach (var spatial in Scene.Spatials)
                spatial.Render();

            Raylib_cs.Raylib.EndMode3D();

            Raylib_cs.Raylib.EndTextureMode();
            Raylib_cs.Raylib.DrawTexture(renderTexture.Value.texture, (int)LayoutInfo.RenderPosition.X, (int)LayoutInfo.RenderPosition.Y, Raylib_cs.Color.WHITE);

            base.OnRender();
        }

        //private static Vector3 ToVector3(Vector2 v2) => new(v2.X, 0, v2.Y);
    }
}