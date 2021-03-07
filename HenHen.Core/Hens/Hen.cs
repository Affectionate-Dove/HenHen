// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenHen.Core.Hens
{
    public class Hen
    {
        public int Id;
        public int Health;
        public HenStatistics Statistics;
        public HenType Type;
    }

    public enum HenType
    {
        PROWADZACY, STUDENT, POMOCNIK
    }
}