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
    }

    public struct EnemySpawner2 : IGameComponent
    {
        public EnemySpawner EnemySpawner { get; set; }
        public int EntityId { get; set; }

        public EnemySpawner2(int entityId, float spawnCooldownTime)
        {
            EnemySpawner = new EnemySpawner(spawnCooldownTime);
            EntityId = entityId;
        }
    }
}