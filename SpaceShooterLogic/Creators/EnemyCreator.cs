using Microsoft.Xna.Framework;
using GameEngineCore;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class EnemyCreator
    {
        public static void Create(Vector2 position, Vector2 velocity, GameState state)
        {
            EnemyType type = ChooseEnemyType();
            Vector2 size = DetermineSize();

            switch (type)
            {
                case EnemyType.Gunship:
                    int entityId = state.EntityCount;

                    state.Positions[entityId] = position;
                    state.Velocities[entityId] = velocity;
                    state.Volumes[entityId] = new Rectangle(0, 0, (int)size.X, (int)size.Y);
                    state.Textures[entityId] = AssetsManager.Instance.GetTexture("sprEnemy0");
                    state.Sizes[entityId] = size;
                    state.TimesSinceLastShot[entityId] = 0.0f;
                    state.TimesSinceLastEnemySpawned[entityId] = -0.1f;
                    state.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations("sprEnemy0"), 0, 0.0f);
                    state.ScoreValues[entityId] = 20;
                    state.Tags[entityId] = state.Tags[entityId].SetBits((int)Tag.IsAlive, (int)Tag.DestroyIfOutsideViewport, (int)Tag.EnemyIsShooter);

                    state.EntityCount++;
                    break;

                case EnemyType.Chaser:
                    entityId = state.EntityCount;

                    state.Positions[entityId] = position;
                    state.Velocities[entityId] = velocity;
                    state.Volumes[entityId] = new Rectangle(0, 0, (int)size.X, (int)size.Y);
                    state.Textures[entityId] = AssetsManager.Instance.GetTexture("sprEnemy1");
                    state.Sizes[entityId] = size;
                    state.TimesSinceLastShot[entityId] = -0.1f;
                    state.TimesSinceLastEnemySpawned[entityId] = -0.1f;
                    state.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations("sprEnemy1"), 0, 0.0f);
                    state.ScoreValues[entityId] = 10;
                    state.Tags[entityId] = state.Tags[entityId].SetBits((int)Tag.IsAlive, (int)Tag.DestroyIfOutsideViewport, (int)Tag.EnemyIsChaser); // 81

                    state.EntityCount++;
                    break;

                case EnemyType.Carrier:
                    entityId = state.EntityCount;

                    state.Positions[entityId] = position;
                    state.Velocities[entityId] = velocity;
                    state.Volumes[entityId] = new Rectangle(0, 0, (int)size.X, (int)size.Y);
                    state.Textures[entityId] = AssetsManager.Instance.GetTexture("sprEnemy2");
                    state.Sizes[entityId] = size;
                    state.TimesSinceLastShot[entityId] = -0.1f;
                    state.TimesSinceLastEnemySpawned[entityId] = -0.1f;
                    state.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations("sprEnemy2"), 0, 0.0f);
                    state.ScoreValues[entityId] = 5;
                    state.Tags[entityId] = state.Tags[entityId].SetBits((int)Tag.IsAlive, (int)Tag.DestroyIfOutsideViewport); // 17

                    state.EntityCount++;
                    break;
            }
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

        private enum EnemyType
        {
            Gunship,
            Chaser,
            Carrier
        }
    }
}