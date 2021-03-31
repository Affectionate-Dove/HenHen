// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;
using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public abstract class Drawable
    {
        public IContainer Parent;
        public Vector2 Offset { get; set; }
        public Axes RelativePositionAxes { get; set; }

        public Vector2 Size { get; set; } = Vector2.One;
        public Axes RelativeSizeAxes { get; set; }

        public Vector2 Anchor { get; set; }
        public Vector2 Origin { get; set; }
        public DrawableLayoutInfo LayoutInfo { get; private set; }

        public void Update()
        {
            PreUpdate();
            UpdateLayout();
            PostUpdate();
        }

        public void Render() => OnRender();

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

        protected virtual void PreUpdate()
        {
        }

        protected virtual void PostUpdate()
        {
        }

        protected virtual void OnRender()
        {
        }

        private Vector2 ComputeLocalPosition()
        {
            var pos = Offset;
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