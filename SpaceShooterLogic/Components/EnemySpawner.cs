namespace SpaceShooterLogic.Components
{
    public struct EnemySpawner
    {
        public float SpawnCooldownTime { get; set; } // in milliseconds
        public bool SpawnOnCooldown => SpawnCooldownTime > 0.0f;

        public EnemySpawner(float spawnCooldownTime)
        {
            SpawnCooldownTime = spawnCooldownTime;
        }

        public bool IsNull()
        {
            return SpawnCooldownTime < 0.0f;
        }
    }
}