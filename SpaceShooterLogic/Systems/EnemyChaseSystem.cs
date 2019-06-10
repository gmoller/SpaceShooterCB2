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

            // selection
            if (enemy == null || enemy.Value.Type != EnemyType.Chaser) return;

            // process data
            var enemyTransform = GameState.Transforms[entityId].Value;
            var enemyPosition = enemyTransform.Position;
            var p = GameState.FindPlayer();
            var playerPosition = GameState.Transforms[p.index].Value.Position;
            if (p.player.Status == PlayerStatus.Destroyed)
            {
                GameState.Velocities[entityId] = new Vector2(0.0f, 0.1f);
                enemyTransform.Rotation = 0.0f;
                GameState.Transforms[entityId] = enemyTransform;
            }
            else
            {
                if (Vector2.Distance(enemyPosition, playerPosition) < _rangeToStartChasing)
                {
                    Vector2 direction = playerPosition - enemyPosition;
                    direction.Normalize();
                    var velocity = direction * _chasingMovementSpeed;
                    float rotationSpeed = _rotationSpeed * deltaTime;
                    if (enemyPosition.X < playerPosition.X) // is ChaserShip to the left or right of player?
                    {
                        enemyTransform.Rotation -= rotationSpeed;
                    }
                    else
                    {
                        enemyTransform.Rotation += rotationSpeed;
                    }

                    // update data
                    GameState.Velocities[entityId] = velocity;
                    GameState.Transforms[entityId] = enemyTransform;
                }
            }
        }
    }
}