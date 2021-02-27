using HenHen.Framework.Screens;

namespace HenHen.Framework.VisualTests
{
    public abstract class VisualTestScene : Screen
    {
        public bool IsSceneDone { get; protected set; }

        public VisualTestScene() => RelativeSizeAxes = Graphics2d.Axes.Both;
    }
}
