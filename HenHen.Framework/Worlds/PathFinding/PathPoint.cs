using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Worlds.PathFinding
{
    public record PathPoint(Vector2 Position)
    {
        private readonly List<PathPoint> connections = new(4);

        public IReadOnlyList<PathPoint> Connections => connections;

        public void ConnectWith(PathPoint pathPoint)
        {
            connections.Add(pathPoint);
            pathPoint.connections.Add(this);
        }
    }
}