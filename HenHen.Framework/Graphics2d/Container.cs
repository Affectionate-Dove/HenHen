// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d.Layouts;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public class Container : Drawable, IContainer<Drawable>
    {
        private MarginPadding padding;
        private Axes autoSizeAxes;

        public MarginPadding Padding
        {
            get => padding;
            set
            {
                if (padding.Equals(value))
                    return;

                padding = value;
                ContainerLayoutValid = false;
            }
        }

        public Axes AutoSizeAxes
        {
            get => autoSizeAxes;
            set
            {
                if (autoSizeAxes.Equals(value))
                    return;

                autoSizeAxes = value;
                LayoutValid = false;
            }
        }

        public List<Drawable> Children { get; } = new List<Drawable>();
        IEnumerable<Drawable> IContainer<Drawable>.Children => Children;

        public ContainerLayoutInfo ContainerLayoutInfo { get; protected set; }
        public bool ContainerLayoutValid { get; protected set; }

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

        protected Vector2 ComputeChildrenRenderPosition() => LayoutInfo.RenderRect.TopLeft + Padding.TopLeft;

        protected Vector2 ComputeChildrenRenderSize() => LayoutInfo.RenderSize - Padding.Total;

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (!ContainerLayoutValid)
                UpdateContainerLayout();

            foreach (var child in Children)
                child.Update();
        }

        protected override void OnLayoutUpdate()
        {
            base.OnLayoutUpdate();
            UpdateContainerLayout();
            foreach (var child in Children)
            {
                if (AutoSizeAxes != Axes.None && !child.LayoutValid)
                    LayoutValid = false;
                child.UpdateLayout();
            }
        }

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

        private void UpdateContainerLayout()
        {
            ContainerLayoutInfo = new ContainerLayoutInfo
            {
                ChildrenRenderPosition = ComputeChildrenRenderPosition(),
                ChildrenRenderSize = ComputeChildrenRenderSize()
            };
            ContainerLayoutValid = true;
        }
    }
}