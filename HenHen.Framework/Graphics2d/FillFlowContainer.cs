namespace HenHen.Framework.Graphics2d
{
    public class FillFlowContainer : Container
    {
        public float Spacing { get; set; }
        public Direction Direction { get; set; }

        public override void AddChild(Drawable child)
        {
            Container container = new Container();
            container.AutoSizeAxes = Axes.Both;
            container.AddChild(child);
            base.AddChild(container);
        }

        public override void RemoveChild(Drawable child)
        {
            var container = FindChildContainer(child);
            if (container == null)
                return;
            container.RemoveChild(child);
            base.RemoveChild(container);
        }
        protected override void OnUpdate()
        {
            base.OnUpdate();
            var maxPos = 0f;
            foreach (var child in Children)
            {
                if (Direction == Direction.Horizontal)
                {
                    child.Position = new System.Numerics.Vector2(maxPos, 0);
                    maxPos += child.GetRenderSize().X;
                }
                else
                {
                    child.Position = new System.Numerics.Vector2(0, maxPos);
                    maxPos += child.GetRenderSize().Y;
                }
                maxPos += Spacing;
            }
        }
        private Container FindChildContainer(Drawable child)
        {
            foreach (var drawable in Children)
            {
                var container = drawable as Container;
                if (container.Children[0] == child)
                    return container;
            }
            return null;
        }
    }
    public enum Direction
    {
        Horizontal, Vertical
    }
}
