using HenHen.Framework.Graphics;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Screens
{
    public class ScreenStack : Drawable, IContainer<Screen>
    {
        private readonly List<Screen> screens = new List<Screen>();

        public Screen CurrentScreen => screens[^1];

        public IEnumerable<Screen> Children => screens;

        public Vector2 GetChildrenRenderPosition() => GetRenderPosition();
        public Vector2 GetChildrenRenderSize() => GetRenderSize();

        protected override void OnUpdate()
        {
            base.OnUpdate();
            CurrentScreen.Update();
        }

        protected override void OnRender()
        {
            base.OnRender();
            CurrentScreen.Render();
        }
    }
}
