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
