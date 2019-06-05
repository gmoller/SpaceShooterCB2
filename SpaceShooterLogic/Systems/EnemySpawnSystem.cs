using Microsoft.Xna.Framework;
using SpaceShooterLogic.Creators;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class EnemySpawnSystem : System
    {
        private readonly int _cooldownTime; // milliseconds between enemy spawns
        private readonly float _distanceAboveScreenToSpawn;
        private readonly float _minimumEnemyVelocity; // pixels per millisecond
        private readonly float _maximumEnemyVelocity; // pixels per millisecond

        public EnemySpawnSystem(string name, GameState gameState) : base(name, gameState)
        {
            _cooldownTime = 1000;
            _distanceAboveScreenToSpawn = 0.0f; // 20.0f
            _minimumEnemyVelocity = 0.06f;
            _maximumEnemyVelocity = 0.18f;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var timeElapsedSinceLastEnemySpawned = GameState.TimesSinceLastEnemySpawned[entityId];

            // selection
            if (timeElapsedSinceLastEnemySpawned.IsNegative()) return;

            // process data
            var onCooldown = SpawnerOnCooldown(timeElapsedSinceLastEnemySpawned);
            if (onCooldown)
            {
                timeElapsedSinceLastEnemySpawned += deltaTime;
            }
            else
            {
                // spawn enemy
                var spawnPosition = new Vector2(RandomGenerator.Instance.GetRandomFloat(0, DeviceManager.Instance.ScreenWidth), -_distanceAboveScreenToSpawn);
                float velocity = RandomGenerator.Instance.GetRandomFloat(_minimumEnemyVelocity, _maximumEnemyVelocity);
                EnemyCreator.Create(spawnPosition, new Vector2(0.0f, velocity), GameState);

                // put on cooldown
                timeElapsedSinceLastEnemySpawned = 0.0f;
            }

            // update data
            GameState.TimesSinceLastEnemySpawned[entityId] = timeElapsedSinceLastEnemySpawned;
        }

        private bool SpawnerOnCooldown(float timeElapsedSinceLastEnemySpawned)
        {
            return timeElapsedSinceLastEnemySpawned < _cooldownTime;
        }
    }
}