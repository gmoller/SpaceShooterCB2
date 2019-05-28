using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Creators;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    public class SpawnComponent : UpdateComponent
    {
        private const float DISTANCE_ABOVE_SCREEN_TO_SPAWN = 20.0f;
        private const float MIN_ENEMY_VELOCITY = 0.06f; // pixels per millisecond
        private const float MAX_ENEMY_VELOCITY = 0.18f; // pixels per millisecond

        private readonly int _spawnCooldown; // in milliseconds

        private float _timeElapsedSinceLastEnemySpawned; // in milliseconds

        internal SpawnComponent(int spawnCooldown)
        {
            _spawnCooldown = spawnCooldown;
        }

        public override void Update(float deltaTime)
        {
            if (!OnCooldown(deltaTime))
            {
                SpawnEnemy();
                StartCooldown();
            }
        }

        private bool OnCooldown(float deltaTime)
        {
            if (_timeElapsedSinceLastEnemySpawned < _spawnCooldown)
            {
                _timeElapsedSinceLastEnemySpawned += deltaTime;

                return true;
            }

            return false;
        }

        private void SpawnEnemy()
        {
            var spawnPosition = new Vector2(RandomGenerator.Instance.GetRandomFloat(0, DeviceManager.Instance.ScreenWidth), -DISTANCE_ABOVE_SCREEN_TO_SPAWN);
            float velocity = RandomGenerator.Instance.GetRandomFloat(MIN_ENEMY_VELOCITY, MAX_ENEMY_VELOCITY);
            EnemyCreator.Create(spawnPosition, new Vector2(0.0f, velocity));
        }

        private void StartCooldown()
        {
            _timeElapsedSinceLastEnemySpawned = 0.0f;
        }

        #region Send & Recieve
        private void Send()
        {
        }

        public override void Receive(string attributeName, object payload)
        {
        }
        #endregion
    }
}