using HenHen.Framework.Worlds.PathFinding;
using NUnit.Framework;

namespace HenHen.Framework.Tests.Worlds.PathFinding
{
    public class PathFindingStateTests
    {
        [Test]
        public void NotFinishedTest()
        {
            Assert.IsTrue(PathFindingState.Successful.HasFlag(PathFindingState.Finished));
            Assert.IsTrue(PathFindingState.Failed.HasFlag(PathFindingState.Finished));
            Assert.IsFalse(PathFindingState.NotStarted.HasFlag(PathFindingState.Finished));
            Assert.IsFalse(PathFindingState.InProgress.HasFlag(PathFindingState.Finished));
        }
    }
}