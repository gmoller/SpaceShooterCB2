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
            float scale = DetermineScale();
            var size = new Vector2(16.0f, 16.0f);

            int entityId = state.EntityCount;

            state.GameData.Transforms[entityId] = new Transform(position, 0.0f, new Vector2(scale), size);
            state.GameData.Velocities[entityId] = velocity;
            state.GameData.Volumes[entityId] = new Rectangle(0, 0, (int)(size.X * scale), (int)(size.Y * scale));

            string textureName = string.Empty;
            switch (type)
            {
                case EnemyType.Gunship:
                    state.GameData.Enemies[entityId] = new Enemy(EnemyType.Gunship, 20);
                    state.GameData.Weapons[entityId] = new Weapon(true);
                    textureName = "sprEnemy0";
                    break;

                case EnemyType.Chaser:
                    state.GameData.Enemies[entityId] = new Enemy(EnemyType.Chaser, 10);
                    textureName = "sprEnemy1";
                    break;

                case EnemyType.Carrier:
                    state.GameData.Enemies[entityId] = new Enemy(EnemyType.Carrier, 5);
                    textureName = "sprEnemy2";
                    break;
            }

            state.GameData.Textures[entityId] = AssetsManager.Instance.GetTexture(textureName);
            state.GameData.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations(textureName));
            state.GameData.Tags[entityId] = state.GameData.Tags[entityId].SetBits((int)Tag.IsAlive);

            state.EntityCount++;
            state.AliveEntityCount++;

            Create2(position, velocity, state);
        }

        public static void Create2(Vector2 position, Vector2 velocity, GameState state)
        {
            int entityId = state.GameData2.GetNextEntityId();

            float scale = DetermineScale();
            var size = new Vector2(16.0f, 16.0f);
            state.GameData2.Transform.Add(new Transform2(entityId, position, 0.0f, new Vector2(scale), size));
            state.GameData2.Velocity.Add(new Velocity2(entityId, velocity.X, velocity.Y));
            state.GameData2.Volume.Add(new Volume2(entityId, position.X - 8.0f, position.Y - 8.0f, size.X * scale, size.Y * scale));
            state.GameData2.Texture.Add(new Texture2(entityId, AssetsManager.Instance.GetTexture("sprEnemy2")));
            state.GameData2.AnimationData.Add(new AnimationData2(entityId, AssetsManager.Instance.GetAnimations("sprEnemy2")));
            state.GameData2.Enemy.Add(new Enemy2(entityId, EnemyType.Carrier, 5));
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

        private static float DetermineScale()
        {
            float scale = RandomGenerator.Instance.GetRandomFloat(1.0f, 3.0f);

            return scale;
        }
    }
}