using System.Collections.Generic;

namespace HenHen.Core.Owners
{
    class Owner
    {
        public int Id;
        public string Name;
        public List<int> Hens { get; } = new List<int>();
    }
}
