using System;
using System.Numerics;

namespace HenHen.Framework.Graphics
{
    public class Drawable
    {
        public Vector2 Position { get; set; }
        public Axes RelativePositionAxes { get; set; }

        public Vector2 Size { get; set; }
        public Axes RelativeSizeAxes { get; set; }

        public Vector2 Anchor { get; set; }
        public Vector2 Origin { get; set; }

        public IContainer<Drawable> Parent;

        public Vector2 GetRenderPosition()
        {
            var pos = Position;
            if (RelativePositionAxes.HasFlag(Axes.X))
                pos.X *= Parent.GetChildrenRenderSize().X;
            if (RelativePositionAxes.HasFlag(Axes.Y))
                pos.Y *= Parent.GetChildrenRenderSize().Y;

            pos += Parent.GetChildrenRenderPosition();
            pos += Anchor * Parent.GetChildrenRenderSize();
            return pos;
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
