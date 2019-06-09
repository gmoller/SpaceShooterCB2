using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class PlayerInputSystem : System
    {
        private readonly float _movementSpeed; // pixels per millisecond

        public PlayerInputSystem(string name, GameState gameState) : base(name, gameState)
        {
            _movementSpeed = 0.24f;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var player = GameState.Players[entityId];

            // selection
            if (player.IsNull() || player.Status == PlayerStatus.Destroyed) return;

            // process data
            var direction = Vector2.Zero;
            player.ShootAction = false;
            if (KeyboardHandler.IsKeyDown(Keys.W))
            {
                direction = new Vector2(direction.X, -1.0f);
            }
            if (KeyboardHandler.IsKeyDown(Keys.S))
            {
                direction = new Vector2(direction.X, 1.0f);
            }
            if (KeyboardHandler.IsKeyDown(Keys.A))
            {
                direction = new Vector2(-1.0f, direction.Y);
            }
            if (KeyboardHandler.IsKeyDown(Keys.D))
            {
                direction = new Vector2(1.0f, direction.Y);
            }
            if (KeyboardHandler.IsKeyDown(Keys.Space))
            {
                player.ShootAction = true;
            }

            // update data
            GameState.Velocities[entityId] = direction * _movementSpeed;
            if (player.ShootAction)
            {
                GameState.Players[entityId] = player;
            }
        }
    }
}