// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenFwork.Random
{
    public interface IRandomValueRange<T>
    {
        T Min { get; }
        T Max { get; }

        /// <summary>
        /// Returns a value between <see cref="Min"/> and <see cref="Max"/>.
        /// </summary>
        T GetRandom();
    }
}