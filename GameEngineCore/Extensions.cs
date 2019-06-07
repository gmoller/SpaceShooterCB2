namespace GameEngineCore
{
    public static class Extensions
    {
        public static bool IsBitSet(this byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public static bool IsBitSet(this int i, int pos)
        {
            return (i & (1 << pos)) != 0;
        }

        public static bool AreBitsSet(this byte b, params int[] pos)
        {
            bool bitsSet = false;
            foreach (int p in pos)
            {
                bitsSet = b.IsBitSet(p);
            }

            return bitsSet;
        }

        public static bool AreBitsSet(this int i, params int[] pos)
        {
            bool bitsSet = false;
            foreach (int p in pos)
            {
                bitsSet = i.IsBitSet(p);
            }

            return bitsSet;
        }

        public static byte SetBit(this byte b, int pos)
        {
            return (byte)(b | (1 << pos));
        }

        public static int SetBit(this int i, int pos)
        {
            return i | (1 << pos);
        }

        public static byte SetBits(this byte b, params int[] pos)
        {
            byte b1 = b;
            foreach (int p in pos)
            {
                b1 = b1.SetBit(p);
            }

            return b1;
        }

        public static int SetBits(this int i, params int[] pos)
        {
            int i1 = i;
            foreach (int p in pos)
            {
                i1 = i1.SetBit(p);
            }

            return i1;
        }

        public static byte UnsetBit(this byte b, int pos)
        {
            return (byte)(b & ~(1 << pos));
        }

        public static int UnsetBit(this int i, int pos)
        {
            return i & ~(1 << pos);
        }

        public static byte UnsetBits(this byte b, params int[] pos)
        {
            byte b1 = b;
            foreach (int p in pos)
            {
                b1 = b1.UnsetBit(p);
            }

            return b1;
        }

        public static int UnsetBits(this int i, params int[] pos)
        {
            int i1 = i;
            foreach (int p in pos)
            {
                i1 = i1.UnsetBit(p);
            }

            return i1;
        }

        public static byte ToggleBit(this byte b, int pos)
        {
            return (byte)(b ^ (1 << pos));
        }

        public static int ToggleBit(this int i, int pos)
        {
            return i ^ (1 << pos);
        }

        public static bool IsNegative(this float f)
        {
            return f < 0.0f;
        }
    }
}