using HenHen.Framework.Graphics2d;

namespace HenHen.Framework.Extensions
{
    public static class RaylibColorExtensions
    {
        public static Raylib_cs.Color ToRaylibColor(this ColorInfo c) => new Raylib_cs.Color(c.r, c.g, c.b, c.a);
    }
}
