// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;
using System.Linq;

namespace HenHen.Framework.Graphics2d
{
    public class FillFlowContainer : Container
    {
        private Direction direction;
        private float spacing;

        public float Spacing
        {
            get => spacing;
            set
            {
                if (spacing == value)
                    return;

                spacing = value;
                LayoutValid = false;
            }
        }

        public Direction Direction
        {
            get => direction;
            set
            {
                direction = value;
                foreach (var child in base.Children)
                {
                    var container = child as Container;
                    container.AutoSizeAxes = AxisFromDirection();
                    container.RelativeSizeAxes = AxisPerpendicularToDirection();
                }
                LayoutValid = false;
            }
        }

        public new IEnumerable<Drawable> Children => base.Children.Select(c => (c as Container).Children[0]);

        public override void AddChild(Drawable child)
        {
            var container = new Container
            {
                AutoSizeAxes = AxisFromDirection(),
                RelativeSizeAxes = AxisPerpendicularToDirection()
            };
            container.AddChild(child);
            base.AddChild(container);
            LayoutValid = false;
            ContainerLayoutValid = false;
        }

        public override void RemoveChild(Drawable child)
        {
            var container = FindChildContainer(child);
            if (container == null)
                return;
            container.RemoveChild(child);
            base.RemoveChild(container);
            LayoutValid = false;
            ContainerLayoutValid = false;
        }

        protected override void OnLayoutUpdate()
        {
            base.OnLayoutUpdate();

            foreach (var child in base.Children)
                child.UpdateLayout();

            // TODO: call this after layout change or children layout change
            UpdateChildrenPositions();

            foreach (var child in base.Children)
                child.UpdateLayout();
        }

        private void UpdateChildrenPositions()
        {
            var maxPos = 0f;
            foreach (var child in base.Children)
            {
                if (Direction == Direction.Horizontal)
                {
                    child.Offset = new System.Numerics.Vector2(maxPos, 0);
                    maxPos += child.LayoutInfo.RenderSize.X;
                }
                else
                {
                    child.Offset = new System.Numerics.Vector2(0, maxPos);
                    maxPos += child.LayoutInfo.RenderSize.Y;
                }
                maxPos += Spacing;
            }
        }

        private Container FindChildContainer(Drawable child)
        {
            foreach (var drawable in base.Children)
            {
                var container = drawable as Container;
                if (container.Children[0] == child)
                    return container;
            }
            return null;
        }

        private Axes AxisFromDirection() => Direction switch
        {
            Direction.Horizontal => Axes.X,
            Direction.Vertical => Axes.Y,
            _ => throw new System.NotImplementedException()
        };

        private Axes AxisPerpendicularToDirection() => Direction switch
        {
            Direction.Horizontal => Axes.Y,
            Direction.Vertical => Axes.X,
            _ => throw new System.NotImplementedException()
        };
    }

    public enum Direction
    {
        Horizontal, Vertical
    }
}