using Microsoft.Xna.Framework;

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

        public static bool IsNull(this Vector2 v)
        {
            return v == Vector2.Zero;
        }

        public static bool IsNegative(this float f)
        {
            return f < 0.0f;
        }
    }
}