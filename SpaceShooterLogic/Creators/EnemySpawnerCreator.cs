using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Creators
{
    public static class SpawnCreator
    {
        public static void Create(GameState state)
        {
            int entityId = state.EntityCount;

            state.EnemySpawner[entityId] = new EnemySpawner();
            state.Tags[entityId] = 1;

            state.EntityCount++;
            state.AliveEntityCount++;
        }
    }
}