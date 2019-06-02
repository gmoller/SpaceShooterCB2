namespace SpaceShooterUtilities
{
    public static class Extensions
    {
        public static bool IsBitSet(this byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public static byte SetBit(this byte b, int pos)
        {
            return (byte)(b | (1 << pos));
        }

        public static byte UnsetBit(this byte b, int pos)
        {
            return (byte)(b & ~(1 << pos));
        }

        public static byte ToggleBit(this byte b, int pos)
        {
            return (byte)(b ^ (1 << pos));
        }
    }
}