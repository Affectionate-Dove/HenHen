// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;
using System.Linq;

namespace HenHen.Framework.Random
{
    public record ChanceTable<T>(IReadOnlyList<ChanceTableEntry<T>> Entries)
    {
        private static readonly System.Random random = new System.Random();
        public T GetRandom()
        {
            var currentEndPoint = 0;
            var sum = Entries.Sum(entry => entry.Chance);
            var randomPoint = random.NextDouble() * sum;
            for (var i = 0; i < Entries.Count; i++)
            {
                currentEndPoint += Entries[i].Chance;
                if (randomPoint <= currentEndPoint)
                {
                    return Entries[i].Value;
                }
            }
            throw new System.Exception("Couldn't pick random value.");
        }
    }
}