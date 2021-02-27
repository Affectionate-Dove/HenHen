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

        public DrawableLayoutInfo LayoutInfo { get; private set; }

        private Vector2 ComputeLocalPosition()
        {
            var pos = Position;
            if (Parent is null)
                return pos;

            if (RelativePositionAxes.HasFlag(Axes.X))
                pos.X *= Parent.ContainerLayoutInfo.ChildrenRenderSize.X;
            if (RelativePositionAxes.HasFlag(Axes.Y))
                pos.Y *= Parent.ContainerLayoutInfo.ChildrenRenderSize.Y;
            pos += Anchor * Parent.ContainerLayoutInfo.ChildrenRenderSize;

            return pos;
        }

        private Vector2 ComputeRenderPosition(Vector2 localPosition)
        {
            var pos = localPosition;
            if (Parent != null)
                pos += Parent.ContainerLayoutInfo.ChildrenRenderPosition;
            return pos;
        }

        protected virtual Vector2 ComputeRenderSize()
        {
            var size = Size;
            if (Parent is null)
                return size;

            if (RelativeSizeAxes.HasFlag(Axes.X))
                size.X *= Parent.ContainerLayoutInfo.ChildrenRenderSize.X;
            if (RelativeSizeAxes.HasFlag(Axes.Y))
                size.Y *= Parent.ContainerLayoutInfo.ChildrenRenderSize.Y;

            return size;
        }

        public void Update()
        {
            PreUpdate();
            UpdateLayout();
            PostUpdate();
        }

        protected virtual void PreUpdate() { }

        private void UpdateLayout()
        {
            var localPos = ComputeLocalPosition();
            LayoutInfo = new DrawableLayoutInfo
            {
                Origin = Origin,
                LocalPosition = localPos,
                RenderPosition = ComputeRenderPosition(localPos),
                RenderSize = ComputeRenderSize()
            };
        }

        protected virtual void PostUpdate()
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
