using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Creators
{
    public static class SpawnCreator
    {
        public static void Create(GameState state)
        {
            int entityId = state.EntityCount;

            state.GameData.EnemySpawner[entityId] = new EnemySpawner();
            state.GameData.Tags[entityId] = 1;

            state.EntityCount++;
            state.AliveEntityCount++;
        }
    }
}