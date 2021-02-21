using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public interface IContainer
    {
        public Vector2 GetChildrenRenderPosition();
        public Vector2 GetChildrenRenderSize();
    }

    public interface IContainer<T> : IContainer
    {
        public IEnumerable<T> Children { get; }
    }
}
