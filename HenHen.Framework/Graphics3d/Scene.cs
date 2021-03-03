using System.Collections.Generic;

namespace HenHen.Framework.Graphics3d
{
    public class Scene
    {
        public List<Spatial> Spatials { get; } = new();

        public void Render()
        {
            foreach (var spatial in Spatials)
                spatial.Render();
        }
    }
}