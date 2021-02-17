using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Graphics
{
    public interface IContainer<T>
    {
        public IEnumerable<T> Children { get; }
        public Vector2 GetChildrenRenderPosition();
        public Vector2 GetChildrenRenderSize();
    }
}
