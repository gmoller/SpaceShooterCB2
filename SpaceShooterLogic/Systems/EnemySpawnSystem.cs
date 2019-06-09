using Microsoft.Xna.Framework;
using GameEngineCore;
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
            var enemySpawner = GameState.EnemySpawner[entityId];

            // selection
            if (enemySpawner.IsNull()) return;

            // process data
            var onCooldown = enemySpawner.SpawnOnCooldown;
            if (onCooldown)
            {
                enemySpawner.SpawnCooldownTime -= deltaTime;
                enemySpawner.SpawnCooldownTime = MathHelper.Clamp(enemySpawner.SpawnCooldownTime, 0.0f, _cooldownTime);
            }
            else
            {
                // spawn enemy
                var spawnPosition = new Vector2(RandomGenerator.Instance.GetRandomFloat(0, DeviceManager.Instance.ScreenWidth), -_distanceAboveScreenToSpawn);
                float velocity = RandomGenerator.Instance.GetRandomFloat(_minimumEnemyVelocity, _maximumEnemyVelocity);
                EnemyCreator.Create(spawnPosition, new Vector2(0.0f, velocity), GameState);

                // put on cooldown
                enemySpawner.SpawnCooldownTime = _cooldownTime;
            }

            // update data
            GameState.EnemySpawner[entityId] = enemySpawner;
        }
    }
}