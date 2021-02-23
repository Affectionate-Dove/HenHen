using System;
using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public abstract class Drawable
    {
        public Vector2 Position { get; set; }
        public Axes RelativePositionAxes { get; set; }

        public Vector2 Size { get; set; } = Vector2.One;
        public Axes RelativeSizeAxes { get; set; }

        public Vector2 Anchor { get; set; }
        public Vector2 Origin { get; set; }

        public IContainer Parent;

        public Vector2 GetLocalPosition(bool withAnchor = true)
        {
            var pos = Position;
            if (Parent is null)
                return pos;

            if (RelativePositionAxes.HasFlag(Axes.X))
                pos.X *= Parent.GetChildrenRenderSize().X;
            if (RelativePositionAxes.HasFlag(Axes.Y))
                pos.Y *= Parent.GetChildrenRenderSize().Y;
            if (withAnchor)
                pos += Anchor * Parent.GetChildrenRenderSize();

            return pos;
        }

        public Vector2 GetRenderPosition()
        {
            var pos = GetLocalPosition();
            if (Parent != null)
                pos += Parent.GetChildrenRenderPosition();
            return pos;
        }

        public virtual Vector2 GetRenderSize()
        {
            var size = Size;
            if (Parent is null)
                return size;

            if (RelativeSizeAxes.HasFlag(Axes.X))
                size.X *= Parent.GetChildrenRenderSize().X;
            if (RelativeSizeAxes.HasFlag(Axes.Y))
                size.Y *= Parent.GetChildrenRenderSize().Y;

            return size;
        }

        public System.Drawing.RectangleF GetLocalRect(bool withAnchor = true)
        {
            var localPos = GetLocalPosition(withAnchor);
            var renderSize = GetRenderSize();
            return new System.Drawing.RectangleF(localPos.X, localPos.Y, renderSize.X, renderSize.Y);
        }

        public System.Drawing.RectangleF GetRenderRect()
        {
            var renderPos = GetRenderPosition();
            var renderSize = GetRenderSize();

            renderPos -= renderSize * Origin;
            return new System.Drawing.RectangleF(renderPos.X, renderPos.Y, renderSize.X, renderSize.Y);
        }

        public void Update() => OnUpdate();

        protected virtual void OnUpdate()
        {
        }

        public void Render() => OnRender();

        protected virtual void OnRender()
        {
        }
    }

    [Flags]
    public enum Axes
    {
        None = 0,
        X = 1,
        Y = 2,

        Both = X | Y
    }
}
