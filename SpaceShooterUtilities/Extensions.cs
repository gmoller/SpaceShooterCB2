using Microsoft.Xna.Framework;

namespace SpaceShooterUtilities
{
    public static class Extensions
    {
        public static bool IsNull(this Vector2 v)
        {
            return v == Vector2.Zero;
        }
    }
}