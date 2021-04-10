// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;
using System.Collections.Generic;

namespace HenHen.Framework.Worlds.PathFinding
{
    public class PathfindingAgent
    {
        public List<PathNode> VisitedNodes;
        public PathNode CurrentNode;
        public PathNode Destination;

        public event Action<PathfindingAgent> AgentCreated;

        public event Action<List<PathNode>> PathFound;

        public PathfindingAgent(List<PathNode> visitedNodes, PathNode currentNode, PathNode destination)
        {
            VisitedNodes = visitedNodes;
            CurrentNode = currentNode;
            Destination = destination;
        }

        public void Run()
        {
            if (CurrentNode == Destination)
                OnDestinationEncountered();
            else
                CreateMoreAgents();
        }

        private void CreateMoreAgents()
        {
            foreach (var connectedNode in CurrentNode.Connections)
            {
                if (!VisitedNodes.Contains(connectedNode))
                    CreateAgent(connectedNode);
            }
        }

        private void CreateAgent(PathNode connectedNode)
        {
            List<PathNode> visitedNodes = new(VisitedNodes.Count + 1);
            visitedNodes.AddRange(visitedNodes);
            visitedNodes.Add(connectedNode);
            var newAgent = new PathfindingAgent(visitedNodes, connectedNode, Destination);
            SignalAgentCreation(newAgent);
        }

        private void OnDestinationEncountered() => PathFound(VisitedNodes);

        private void SignalAgentCreation(PathfindingAgent newAgent) => AgentCreated(newAgent);
    }
}