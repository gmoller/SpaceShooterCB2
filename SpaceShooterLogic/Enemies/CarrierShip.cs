using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Enemies
{
    public class CarrierShip : Enemy
    {
        public CarrierShip(string textureName, Vector2 position, Vector2 velocity) : base(textureName, position, velocity)
        {
        }

        public override int Score => 5;

        public override void UseSpecialPower(GameTime gameTime)
        {
            // no special power, do nothing
        }
    }
}