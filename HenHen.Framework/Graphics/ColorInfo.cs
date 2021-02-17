namespace HenHen.Framework.Graphics
{
    public struct ColorInfo
    {
        public byte a;
        public byte r;
        public byte b;
        public byte g;

        public ColorInfo(byte r, byte g, byte b, byte a = 255)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }
}
