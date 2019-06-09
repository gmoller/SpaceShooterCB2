using Microsoft.Xna.Framework;
using GameEngineCore;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class EnemyCreator
    {
        public static void Create(Vector2 position, Vector2 velocity, GameState state)
        {
            EnemyType type = ChooseEnemyType();
            Vector2 size = DetermineSize();

            int entityId = state.EntityCount;

            state.Positions[entityId] = position;
            state.Velocities[entityId] = velocity;
            state.Volumes[entityId] = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            state.Sizes[entityId] = size;
            state.TimesSinceLastEnemySpawned[entityId] = -0.1f;

            switch (type)
            {
                case EnemyType.Gunship:
                    state.Textures[entityId] = AssetsManager.Instance.GetTexture("sprEnemy0");
                    state.TimesSinceLastShot[entityId] = 0.0f;
                    state.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations("sprEnemy0"), 0, 0.0f);
                    state.Enemies[entityId] = new Enemy(EnemyType.Gunship, 20);
                    break;

                case EnemyType.Chaser:
                    state.Textures[entityId] = AssetsManager.Instance.GetTexture("sprEnemy1");
                    state.TimesSinceLastShot[entityId] = -0.1f;
                    state.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations("sprEnemy1"), 0, 0.0f);
                    state.Enemies[entityId] = new Enemy(EnemyType.Chaser, 10);
                    break;

                case EnemyType.Carrier:
                    state.Textures[entityId] = AssetsManager.Instance.GetTexture("sprEnemy2");
                    state.TimesSinceLastShot[entityId] = -0.1f;
                    state.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations("sprEnemy2"), 0, 0.0f);
                    state.Enemies[entityId] = new Enemy(EnemyType.Carrier, 5);
                    break;
            }

            state.Tags[entityId] = state.Tags[entityId].SetBits((int)Tag.IsAlive);

            state.EntityCount++;
            state.AliveEntityCount++;
        }

        private static EnemyType ChooseEnemyType()
        {
            int choice = RandomGenerator.Instance.GetRandomInt(1, 10);
            if (choice <= 3)
            {
                return EnemyType.Gunship;
            }
            if (choice >= 5)
            {
                return EnemyType.Chaser;
            }

            return EnemyType.Carrier;
        }

        private static Vector2 DetermineSize()
        {
            float scale = RandomGenerator.Instance.GetRandomFloat(1.0f, 3.0f);
            Vector2 size = new Vector2(16.0f, 16.0f) * scale;

            return size;
        }
    }
}