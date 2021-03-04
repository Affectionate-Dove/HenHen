using System;
using System.Collections.Generic;

namespace HenHen.Framework.Worlds.PathFinding
{
    /// <summary>
    /// Responsible for finding the shortest path
    /// between two given points A and B.
    /// </summary>
    public class Pathfinder
    {
        private readonly List<PathPoint> result = new();

        public PathfindingState State { get; protected set; } = PathfindingState.NotStarted;

        /// <summary>
        /// The found requested path.
        /// Directly connected points that lead from
        /// <seealso cref="PathRequest.Start"/> point to
        /// <see cref="PathRequest.End"/> point
        /// of the <see cref="Request"/> property.
        /// </summary>
        public IReadOnlyList<PathPoint> Result => (State == PathfindingState.Successful) ? result : null;

        public PathRequest Request { get; }

        public Pathfinder(PathRequest request) => Request = request;

        /// <summary>
        /// Performs a bit of pathfinding computations.
        /// </summary>
        public void Update() => throw new NotImplementedException();
    }
}