using System.Numerics;

namespace HenHen.Framework.Graphics3d
{
    public abstract class Spatial
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public void Render()
        {
            OnRender();
        }

        protected virtual void OnRender()
        {
        }

        protected virtual void OnUpdate()
        {
        }
    }
}