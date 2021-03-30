// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;
using System.Linq;

namespace HenHen.Framework.Random
{
    public record ChanceTable<T>(IReadOnlyCollection<ChanceTableEntry<T>> Entries)
    {
        private static readonly System.Random random = new();

        public T GetRandom()
        {
            var currentEndPoint = 0;
            var sum = Entries.Sum(entry => entry.Chance);
            var randomPoint = random.NextDouble() * sum;

            foreach (var entry in Entries)
            {
                currentEndPoint += entry.Chance;
                if (randomPoint < currentEndPoint)
                    return entry.Value;
            }

            throw new System.Exception("Couldn't pick a random value.");
        }
    }
}