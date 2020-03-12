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
            var type = ChooseEnemyType();
            var scale = DetermineScale();
            var size = new Vector2(16.0f, 16.0f);

            var entityId = state.EntityCount;

            state.GameData.Transforms[entityId] = new Transform(position, Color.White, 0.0f, new Vector2(scale), size);
            state.GameData.Velocities[entityId] = velocity;
            state.GameData.Volumes[entityId] = new Rectangle(0, 0, (int)(size.X * scale), (int)(size.Y * scale));

            var textureName = string.Empty;
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
        }

        private static EnemyType ChooseEnemyType()
        {
            var choice = RandomGenerator.Instance.GetRandomInt(1, 10);
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
            var scale = RandomGenerator.Instance.GetRandomFloat(1.0f, 3.0f);

            return scale;
        }
    }
}