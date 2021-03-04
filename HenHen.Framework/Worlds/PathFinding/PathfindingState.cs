using System;

namespace HenHen.Framework.Worlds.PathFinding
{
    [Flags]
    public enum PathfindingState
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