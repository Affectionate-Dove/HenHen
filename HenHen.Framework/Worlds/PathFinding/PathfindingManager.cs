// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;

namespace HenHen.Framework.Worlds.PathFinding
{
    public class PathfindingManager
    {
        private int i = 0;
        public List<Pathfinder> Pathfinders { get; } = new();

        public void Update()
        {
            if (i == Pathfinders.Count)
                i = 0;
            Pathfinders[i].Update();
            if (Pathfinders[i].State.HasFlag(PathfindingState.Finished))
                Pathfinders.RemoveAt(i);
            else
                i++;
        }
    }
}