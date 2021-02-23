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
