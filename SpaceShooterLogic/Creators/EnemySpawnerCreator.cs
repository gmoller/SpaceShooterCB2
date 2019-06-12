using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class EnemySpawnerCreator
    {
        public static void Create(GameState state)
        {
            int entityId = state.EntityCount;

            state.GameData.EnemySpawner[entityId] = new EnemySpawner();
            state.GameData.Tags[entityId] = 1;

            state.EntityCount++;
            state.AliveEntityCount++;
        }

        public static void Create2(GameState state)
        {
            int entityId = state.GameData2.GetNextEntityId();

            state.GameData2.EnemySpawner.Add(new EnemySpawner2(entityId, 1000.0f));
        }
    }

    public static class TransformOnlyCreator
    {
        public static void Create2(GameState state)
        {
            int entityId = state.GameData2.GetNextEntityId();

            state.GameData2.Transform.Add(new Transform2(entityId, new Vector2(50.0f, 600.0f), 0.0f, new Vector2(2.5f), new Vector2(16.0f, 16.0f)));
        }
    }

    public static class TextureOnlyCreator
    {
        public static void Create2(GameState state)
        {
            int entityId = state.GameData2.GetNextEntityId();

            state.GameData2.Texture.Add(new Texture2(entityId, AssetsManager.Instance.GetTexture("sprEnemy2")));
        }
    }
}