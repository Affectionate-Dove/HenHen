using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public class Container : Drawable, IContainer<Drawable>
    {
        public Axes AutoSizeAxes { get; set; }

        protected List<Drawable> Children { get; } = new List<Drawable>();

        public MarginPadding Padding;

        public Vector2 GetChildrenRenderPosition() => GetRenderPosition() + Padding.TopLeft;

        public Vector2 GetChildrenRenderSize() => GetRenderSize() - Padding.Total;

        IEnumerable<Drawable> IContainer<Drawable>.Children => Children;

        public void AddChild(Drawable child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        public void RemoveChild(Drawable child)
        {
            if (Children.Remove(child))
                child.Parent = null;
        }

        protected override void OnUpdate()
        {
            foreach (var child in Children)
                child.Update();
            base.OnUpdate();
        }

        protected override void OnRender()
        {
            foreach (var child in Children)
                child.Render();
            base.OnRender();
        }

        public override Vector2 GetRenderSize()
        {
            var renSize = base.GetRenderSize();
            if (AutoSizeAxes == Axes.None)
                return renSize;

            var maxX = 0f;
            var maxY = 0f;
            foreach (var child in Children)
            {
                var childLocalRect = child.GetLocalRect(false);
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
