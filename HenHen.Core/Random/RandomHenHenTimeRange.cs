// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Worlds;
using HenHen.Framework.Random;

namespace HenHen.Core.Random
{
    public record RandomHenHenTimeRange(HenHenTime Min, HenHenTime Max) : IRandomValueRange<HenHenTime>
    {
        private static readonly System.Random random = new();

        public HenHenTime GetRandom()
        {
            var percentage = random.NextDouble();
            var percentageAsTime = (Max - Min) * percentage;
            return Min + percentageAsTime;
        }
    }
}