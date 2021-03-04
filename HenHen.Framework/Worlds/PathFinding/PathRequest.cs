namespace HenHen.Framework.Worlds.PathFinding
{
    /// <summary>
    /// Represents a request for a path to be found
    /// between two points.
    /// </summary>
    public record PathRequest(PathNode Start, PathNode End);
}