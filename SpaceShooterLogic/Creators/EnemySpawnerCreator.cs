using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Creators
{
    public static class EnemySpawnerCreator
    {
        public static void Create(GameState state)
        {
            var entityId = state.EntityCount;

            state.GameData.EnemySpawner[entityId] = new EnemySpawner();
            state.GameData.Tags[entityId] = 1;

            state.EntityCount++;
            state.AliveEntityCount++;
        }
    }
}