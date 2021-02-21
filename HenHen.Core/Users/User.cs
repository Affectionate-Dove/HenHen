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
