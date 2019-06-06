namespace SpaceShooterLogic.Creators
{
    public static class SpawnCreator
    {
        public static void Create(GameState state)
        {
            int entityId = state.EntityCount;

            state.TimesSinceLastShot[entityId] = -0.1f;
            state.TimesSinceLastEnemySpawned[entityId] = float.MaxValue;
            state.Tags[entityId] = 1;

            state.EntityCount++;
        }
    }
}