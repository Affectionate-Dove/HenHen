// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;

namespace HenHen.Core.Owners
{
    public class Owner
    {
        public int Id;
        public string Name;
        public List<int> Hens { get; } = new List<int>();
    }
}