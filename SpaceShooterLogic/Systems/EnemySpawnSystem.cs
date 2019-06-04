using Microsoft.Xna.Framework;
using SpaceShooterLogic.Creators;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class EnemySpawnSystem : System
    {
        private const int SPAWN_COOLDOWN_TIME = 1000; // milliseconds between enemy spawns
        private const float DISTANCE_ABOVE_SCREEN_TO_SPAWN = 20.0f;
        private const float MIN_ENEMY_VELOCITY = 0.06f; // pixels per millisecond
        private const float MAX_ENEMY_VELOCITY = 0.18f; // pixels per millisecond

        public EnemySpawnSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            #region gather data

            var timeElapsedSinceLastEnemySpawned = GameState.TimesSinceLastEnemySpawned[entityId];

            #endregion

            if (timeElapsedSinceLastEnemySpawned.IsNegative()) return;

            #region process data

            // if time to spawn, spawn enemy, else start cooldown
            // check if we are on cooldown
            var onCooldown = SpawnerOnCooldown(timeElapsedSinceLastEnemySpawned);
            if (onCooldown)
            {
                timeElapsedSinceLastEnemySpawned += deltaTime;
            }
            else
            {
                // spawn enemy
                var spawnPosition = new Vector2(RandomGenerator.Instance.GetRandomFloat(0, DeviceManager.Instance.ScreenWidth), -DISTANCE_ABOVE_SCREEN_TO_SPAWN);
                float velocity = RandomGenerator.Instance.GetRandomFloat(MIN_ENEMY_VELOCITY, MAX_ENEMY_VELOCITY);
                EnemyCreator.Create(spawnPosition, new Vector2(0.0f, velocity));
            }

            #endregion

            #region update data

            GameState.TimesSinceLastEnemySpawned[entityId] = timeElapsedSinceLastEnemySpawned;

            #endregion
        }

        private bool SpawnerOnCooldown(float timeElapsedSinceLastEnemySpawned)
        {
            return timeElapsedSinceLastEnemySpawned < SPAWN_COOLDOWN_TIME;
        }
    }
}