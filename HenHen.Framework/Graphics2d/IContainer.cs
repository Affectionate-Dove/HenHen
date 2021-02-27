using HenHen.Framework.Graphics2d.Layouts;
using System.Collections.Generic;

namespace HenHen.Framework.Graphics2d
{
    public interface IContainer
    {
        ContainerLayoutInfo ContainerLayoutInfo { get; }
    }

    public interface IContainer<T> : IContainer
    {
        public IEnumerable<T> Children { get; }
    }
}
