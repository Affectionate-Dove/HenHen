// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;

namespace HenHen.Framework.Worlds.PathFinding
{
    /// <summary>
    /// Responsible for finding the shortest path
    /// between two given points A and B.
    /// </summary>
    public class Pathfinder
    {
        private readonly List<PathfindingAgent> pathfindingAgents = new();
        private List<PathNode> result = new();
        public PathfindingState State { get; protected set; } = PathfindingState.NotStarted;

        /// <summary>
        /// The found requested path.
        /// Directly connected points that lead from
        /// <seealso cref="PathRequest.Start"/> point to
        /// <see cref="PathRequest.End"/> point
        /// of the <see cref="Request"/> property.
        /// </summary>
        public IReadOnlyList<PathNode> Result => (State == PathfindingState.Successful) ? result : null;

        public PathRequest Request { get; }

        public Pathfinder(PathRequest request)
        {
            Request = request;
            State = PathfindingState.InProgress;
            var agent = new PathfindingAgent(Request.Start, Request.End);
            Agent_AgentCreated(agent);
        }

        /// <summary>
        /// Performs a bit of pathfinding computations.
        /// </summary>
        public void Update(int limit)
        {
            for (var i = 0; i < limit; i++)
            {
                var agent = pathfindingAgents[0];
                agent.Run();
                pathfindingAgents.RemoveAt(0);
                if (State == PathfindingState.Successful)
                    break;
            }
            if (State != PathfindingState.Successful && pathfindingAgents.Count == 0)
                State = PathfindingState.Failed;
        }

        private void Agent_AgentCreated(PathfindingAgent obj)
        {
            obj.PathFound += Agent_PathFound;
            obj.AgentCreated += Agent_AgentCreated;
            pathfindingAgents.Add(obj);
        }

        private void Agent_PathFound(List<PathNode> obj)
        {
            result = obj;
            State = PathfindingState.Successful;
        }
    }
}