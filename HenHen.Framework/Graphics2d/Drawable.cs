// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Numerics;
using System;
using System.Numerics;

namespace HenHen.Framework.Graphics2d
{
    public abstract class Drawable
    {
        private IContainer parent;
        private Vector2 offset;
        private Axes relativePositionAxes;
        private Vector2 size = Vector2.One;
        private Axes relativeSizeAxes;
        private Vector2 anchor;
        private Vector2 origin;
        private bool masking;

        public IContainer Parent
        {
            get => parent;
            set
            {
                if (parent == value)
                    return;

                LayoutValid = false;
                parent = value;
            }
        }

        public Vector2 Offset
        {
            get => offset;
            set
            {
                if (offset == value)
                    return;

                offset = value;
                LayoutValid = false;
            }
        }

        public Axes RelativePositionAxes
        {
            get => relativePositionAxes;
            set
            {
                if (relativePositionAxes == value)
                    return;

                relativePositionAxes = value;
                LayoutValid = false;
            }
        }

        public Vector2 Size
        {
            get => size;
            set
            {
                if (size == value)
                    return;

                size = value;
                LayoutValid = false;
            }
        }

        public Axes RelativeSizeAxes
        {
            get => relativeSizeAxes;
            set
            {
                if (relativeSizeAxes == value)
                    return;

                relativeSizeAxes = value;
                LayoutValid = false;
            }
        }

        public Vector2 Anchor
        {
            get => anchor;
            set
            {
                if (anchor == value)
                    return;

                anchor = value;
                LayoutValid = false;
            }
        }

        public Vector2 Origin
        {
            get => origin;
            set
            {
                if (origin == value)
                    return;

                origin = value;
                LayoutValid = false;
            }
        }

        /// <summary>
        ///     Whether anything outside
        ///     the <see cref="DrawableLayoutInfo.RenderRect"/>
        ///     is hidden.
        /// </summary>
        /// <remarks>
        ///     This doesn't have an effect if this is a top-level Drawable,
        ///     in that case masking is always on.
        /// </remarks>
        public bool Masking
        {
            get => masking;
            set
            {
                if (masking == value)
                    return;

                masking = value;
                LayoutValid = false;
            }
        }

        public DrawableLayoutInfo LayoutInfo { get; private set; }
        public bool LayoutValid { get; protected set; }

        public void Update()
        {
            if (!LayoutValid)
                UpdateLayout();

            OnUpdate();

            if (!LayoutValid)
                UpdateLayout();
        }

        public void Render()
        {
            var _mask = LayoutInfo.Mask;
            if (_mask is null)
                return;

            var mask = _mask.Value;
            Raylib_cs.Raylib.BeginScissorMode((int)mask.Left, (int)mask.Top, (int)mask.Width, (int)mask.Height);

            OnRender();
        }

        public void UpdateLayout()
        {
            var localPos = ComputeLocalPosition();
            var renderPos = ComputeRenderPosition(localPos);
            var renderSize = ComputeRenderSize();
            var renderRect = RectangleF.FromPositionAndSize(renderPos, renderSize, Origin, CoordinateSystem2d.YDown);

            LayoutInfo = new DrawableLayoutInfo
            {
                Origin = Origin,
                LocalPosition = localPos,
                RenderPosition = renderPos,
                RenderSize = renderSize,
                Mask = ComputeMaskArea(renderRect)
            };
            LayoutValid = true;

            OnLayoutUpdate();
        }

        protected virtual Vector2 ComputeRenderSize()
        {
            var size = Size;
            if (Parent is null)
                return size;

            if (RelativeSizeAxes.HasFlag(Axes.X))
                size.X *= Parent.ContainerLayoutInfo.ChildrenRenderArea.Size.X;
            if (RelativeSizeAxes.HasFlag(Axes.Y))
                size.Y *= Parent.ContainerLayoutInfo.ChildrenRenderArea.Size.Y;

            return size;
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnLayoutUpdate()
        {
        }

        protected virtual void OnRender()
        {
        }

        protected RectangleF? ComputeMaskArea(RectangleF renderArea)
        {
            if (Parent is null) // top level Drawable
                return renderArea;

            var maskArea = Parent.ContainerLayoutInfo.MaskArea;

            if (!Masking)
                return maskArea;

            return maskArea?.GetIntersection(renderArea);
        }

        private Vector2 ComputeLocalPosition()
        {
            var pos = Offset;
            if (Parent is null)
                return pos;

            if (RelativePositionAxes.HasFlag(Axes.X))
                pos.X *= Parent.ContainerLayoutInfo.ChildrenRenderArea.Size.X;
            if (RelativePositionAxes.HasFlag(Axes.Y))
                pos.Y *= Parent.ContainerLayoutInfo.ChildrenRenderArea.Size.Y;
            pos += Anchor * Parent.ContainerLayoutInfo.ChildrenRenderArea.Size;

            return pos;
        }

        private Vector2 ComputeRenderPosition(Vector2 localPosition)
        {
            var pos = localPosition;
            if (Parent != null)
                // TODO: check if this is correct
                pos += Parent.ContainerLayoutInfo.ChildrenRenderArea.TopLeft;
            return pos;
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