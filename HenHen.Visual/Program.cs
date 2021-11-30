// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenFwork;

namespace HenHen.Visual
{
    internal class Program
    {
        private static void Main()
        {
            var game = new HenHenGame();
            Host.Run(game);
        }
    }
}