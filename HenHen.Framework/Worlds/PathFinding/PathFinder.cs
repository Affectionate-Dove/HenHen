using System;
using System.Collections.Generic;

namespace HenHen.Framework.Worlds.PathFinding
{
    public class PathFinder
    {
        private readonly List<PathPoint> result = new();

        public PathFinder(PathRequest request)
        {
        }

        public PathFindingState State { get; protected set; } = PathFindingState.NotStarted;

        public IReadOnlyList<PathPoint> Result => (State == PathFindingState.Successful) ? result : null;

        public void Update() => throw new NotImplementedException();
    }

    [Flags]
    public enum PathFindingState
    {
        NotStarted =
            0b0000,

        InProgress =
            0b0010,

        Failed =
            0b1001,

        Successful =
            0b0101,

        Finished = Failed & Successful
    }
}