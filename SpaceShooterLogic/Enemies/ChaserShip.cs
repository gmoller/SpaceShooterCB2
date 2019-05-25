using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Enemies
{
    public class ChaserShip : Enemy
    {
        private const float RANGE_TO_START_CHASING = 320.0f;
        private const float CHASING_MOVE_SPEED = 180.0f; // pixels per second
        private const float ROTATION_SPEED = 0.5f;

        public enum ChasingState
        {
            MoveDown,
            Chase
        }

        private ChasingState _state = ChasingState.MoveDown;

        public ChaserShip(string textureName, Vector2 position, Vector2 velocity) : base(textureName, position, velocity)
        {
            Angle = 0;
        }

        public override int Score => 10;

        public override void UseSpecialPower(GameTime gameTime)
        {
            //Player player = GameEntitiesManager.Instance.Player;
            //var player = Registrar.Instance.GetComponentsForEntity(1);
            //player[ComponentType.Physics]

            if (Vector2.Distance(Position, new Vector2(320.0f, 200.0f)) < RANGE_TO_START_CHASING)
            {
                _state = ChasingState.Chase;
                IsRotatable = true;
            }
            if (_state == ChasingState.Chase)
            {
                Vector2 direction = new Vector2(320.0f, 200.0f) - Position;
                direction.Normalize();
                Body.Velocity = direction * CHASING_MOVE_SPEED;
                float rotationSpeed = ROTATION_SPEED * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (Position.X < 320.0f) // is ChaserShip to the left or right of player?
                {
                    Angle -= rotationSpeed;
                }
                else
                {
                    Angle += rotationSpeed;
                }
            }
        }
    }
}