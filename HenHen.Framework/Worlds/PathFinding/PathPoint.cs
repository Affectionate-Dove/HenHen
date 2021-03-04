using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Worlds.PathFinding
{
    /// <summary>
    /// Represents a point in 2D space that
    /// can be connected to other points.
    /// </summary>
    public record PathPoint(Vector2 Position)
    {
        private readonly HashSet<PathPoint> connections = new(4);

        /// <remarks>Direct connections.</remarks>
        public IReadOnlySet<PathPoint> Connections => connections;

        /// <summary>
        /// Creates a 2-way connection between this
        /// <see cref="PathPoint"/> and the <paramref name="other"/>.
        /// </summary>
        public void ConnectSymmetrically(PathPoint other)
        {
            connections.Add(other);
            other.connections.Add(this);
        }

        /// <summary>
        /// Creates a 1-way connection from this
        /// <see cref="PathPoint"/> to the <paramref name="other"/>.
        /// </summary>
        /// <param name="forceAsymmetry">
        /// If true, if <paramref name="other"/> is connected to
        /// this <see cref="PathPoint"/>, it will be disconnected
        /// to force a 1-way connection.
        /// If false, it's possible to have a 2-way connection
        /// between these points after this function.
        /// </param>
        public void ConnectAsymmetrically(PathPoint other, bool forceAsymmetry = true)
        {
            connections.Add(other);
            if (!forceAsymmetry)
                other.connections.Remove(this);
        }

        /// <summary>
        /// Removes a connection from this
        /// <see cref="PathPoint"/> to the
        /// <paramref name="other"/>, and vice versa.
        /// </summary>
        public void DisconnectSymmetrically(PathPoint other)
        {
            connections.Remove(other);
            other.connections.Remove(other);
        }

        /// <summary>
        /// Removes a connection from this
        /// <see cref="PathPoint"/> to the
        /// <paramref name="other"/>, but leaves a
        /// connection from the <paramref name="other"/>
        /// to this <see cref="PathPoint"/>, if it exists.
        /// </summary>
        public void DisconnectAsymmetrically(PathPoint other) => connections.Remove(other);
    }
}