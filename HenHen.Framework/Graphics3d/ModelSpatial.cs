// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;

namespace HenHen.Framework.Graphics3d
{
    public class ModelSpatial : Spatial
    {
        private Raylib_cs.Model model;

        public ColorInfo Color { get; set; } = new ColorInfo(255, 255, 255);

        public void SetModel(string path) => model = Game.ModelStore.Get(path);

        protected override void OnRender()
        {
            base.OnRender();
            Raylib_cs.Raylib.DrawModel(model, Position, 1, Color);
        }
    }
}