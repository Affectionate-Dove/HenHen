using HenHen.Framework.Graphics2d.Layouts;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public class Container : Drawable, IContainer<Drawable>
    {
        public Axes AutoSizeAxes { get; set; }

        public List<Drawable> Children { get; } = new List<Drawable>();

        public MarginPadding Padding;

        protected Vector2 ComputeChildrenRenderPosition() => LayoutInfo.RenderPosition + Padding.TopLeft;

        protected Vector2 ComputeChildrenRenderSize() => LayoutInfo.RenderSize - Padding.Total;

        IEnumerable<Drawable> IContainer<Drawable>.Children => Children;

        public ContainerLayoutInfo ContainerLayoutInfo { get; protected set; }

        public virtual void AddChild(Drawable child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        public virtual void RemoveChild(Drawable child)
        {
            if (Children.Remove(child))
                child.Parent = null;
        }

        protected override void PreUpdate()
        {
            base.PreUpdate();
            UpdateContainerLayoutInfo();
            foreach (var child in Children)
                child.Update();
            UpdateContainerLayoutInfo();
        }

        protected override void PostUpdate()
        {
            base.PostUpdate();
            UpdateContainerLayoutInfo();
            foreach (var child in Children)
                child.Update();
            UpdateContainerLayoutInfo();
        }

        private void UpdateContainerLayoutInfo() => ContainerLayoutInfo = new ContainerLayoutInfo
        {
            ChildrenRenderPosition = ComputeChildrenRenderPosition(),
            ChildrenRenderSize = ComputeChildrenRenderSize()
        };

        protected override void OnRender()
        {
            foreach (var child in Children)
                child.Render();
            base.OnRender();
        }

        protected override Vector2 ComputeRenderSize()
        {
            var renSize = base.ComputeRenderSize();
            if (AutoSizeAxes == Axes.None)
                return renSize;

            var maxX = 0f;
            var maxY = 0f;
            foreach (var child in Children)
            {
                var childLocalRect = child.LayoutInfo.LocalRect;
                if (AutoSizeAxes.HasFlag(Axes.X) && !child.RelativeSizeAxes.HasFlag(Axes.X))
                    maxX = Math.Max(maxX, childLocalRect.Right * (1 - child.Anchor.X));
                if (AutoSizeAxes.HasFlag(Axes.Y) && !child.RelativeSizeAxes.HasFlag(Axes.Y))
                    maxY = Math.Max(maxY, childLocalRect.Bottom * (1 - child.Anchor.Y));
            }
            if (AutoSizeAxes.HasFlag(Axes.X))
                renSize.X = maxX + Padding.TotalHorizontal;
            if (AutoSizeAxes.HasFlag(Axes.Y))
                renSize.Y = maxY + Padding.TotalVertical;
            return renSize;
        }
    }
}
