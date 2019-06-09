using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Systems
{
    public class EnemyChaseSystem : System
    {
        private readonly float _rangeToStartChasing;
        private readonly float _chasingMovementSpeed; // pixels per millisecond
        private readonly float _rotationSpeed;

        public EnemyChaseSystem(string name, GameState gameState) : base(name, gameState)
        {
            _rangeToStartChasing = 320.0f;
            _chasingMovementSpeed = 0.2f;
            _rotationSpeed = 0.005f;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var enemy = GameState.Enemies[entityId];
            var position = GameState.Positions[entityId];

            // selection
            if (enemy.IsNull() || enemy.Type != EnemyType.Chaser) return;

            // process data
            var p = GameState.FindPlayer();
            var playerPosition = GameState.Positions[p.index];
            if (p.player.Status == PlayerStatus.Destroyed)
            {
                GameState.Velocities[entityId] = new Vector2(0.0f, 0.1f);
                GameState.Rotations[entityId] = 0.0f;
            }
            else
            {
                if (Vector2.Distance(position, playerPosition) < _rangeToStartChasing)
                {
                    Vector2 direction = playerPosition - position;
                    direction.Normalize();
                    var velocity = direction * _chasingMovementSpeed;
                    float rotationSpeed = _rotationSpeed * deltaTime;
                    var angle = GameState.Rotations[entityId];
                    if (position.X < playerPosition.X) // is ChaserShip to the left or right of player?
                    {
                        angle -= rotationSpeed;
                    }
                    else
                    {
                        angle += rotationSpeed;
                    }

                    // update data
                    GameState.Velocities[entityId] = velocity;
                    GameState.Rotations[entityId] = angle;
                }
            }
        }
    }
}