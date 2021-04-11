// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d.Worlds;
using HenHen.Framework.Worlds;

namespace HenHen.Framework.VisualTests.Graphics2d.Worlds
{
    public class WorldViewer2dTestScene : VisualTestScene
    {
        public WorldViewer2dTestScene()
        {
            var worldViewer2d = new WorldViewer2d(new World())
            {
                Size = new System.Numerics.Vector2(400)
            };
            AddChild(worldViewer2d);
        }
    }
}