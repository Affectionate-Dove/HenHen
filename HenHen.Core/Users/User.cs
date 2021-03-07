// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;

namespace HenHen.Core.Users
{
    public class User
    {
        public string Name;
        public int Id;
        public List<int> Owners { get; } = new List<int>();
    }
}