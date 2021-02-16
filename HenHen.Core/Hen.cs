namespace HenHen.Core
{
    public class Hen
    {
        public int Health { get; set; }
        public HenStats Stats { get; } = new HenStats();
        public HenBreed Breed { get; set; }
    }

    public enum HenBreed
    {
        Hizard
    }
}
