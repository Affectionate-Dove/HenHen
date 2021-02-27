namespace HenHen.Framework.VisualTests
{
    public class VisualTestsGame : Game
    {
        public VisualTestsGame() => ScreenStack.Push(new VisualTester());
    }
}
