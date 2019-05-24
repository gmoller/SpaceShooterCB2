using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooterLogic.Enemies
{
    public class InvisibleShip : Enemy
    {
        public InvisibleShip(Texture2D texture, Vector2 position, Vector2 velocity) : base(texture, position, velocity)
        {
        }

        public override int Score => 1;

        public override void UseSpecialPower()
        {
            // no special power, do nothing
        }
    }
}