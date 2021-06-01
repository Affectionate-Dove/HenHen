// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenHen.Framework.Random
{
    public static class RNG
    {
        private static readonly System.Random random = new();

        /// <param name="min">Inclusive lower bound.</param>
        /// <param name="max">Inclusive upper bound.</param>
        public static int GetInt(int min, int max) => random.Next(min, max + 1);

        /// <param name="upperBound">Exclusive upper bound.</param>
        public static int GetIntBelow(int upperBound) => random.Next(upperBound);

        /// <returns>A random number >= 0 and < 1.</returns>
        public static double GetDouble() => random.NextDouble();

        /// <param name="min">Inclusive lower bound.</param>
        /// <param name="max">Exclusive upper bound.</param>
        public static double GetDouble(double min, double max) => min + (random.NextDouble() * (max - min));
    }
}