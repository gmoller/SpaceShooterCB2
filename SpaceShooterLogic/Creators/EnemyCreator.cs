using GameEngineCore;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class EnemyCreator
    {
        public static Entity2 Create(Vector2 position, Vector2 velocity)
        {
            EnemyType type = ChooseEnemyType();
            Vector2 size = DetermineSize();

            Entity2 enemy = null;
            switch (type)
            {
                case EnemyType.Gunship:
                    var components = new ComponentsSet(12000.0f);
                    components.AddComponent(typeof(EnemyPhysicsComponent), new EnemyPhysicsComponent(position, velocity, size, 20));
                    components.AddComponent(typeof(SpriteComponent), new SpriteComponent("sprEnemy0"));
                    components.AddComponent(typeof(GraphicsComponent), new GraphicsComponent("sprEnemy0", position, size));
                    //components.AddComponent(typeof(VolumeGraphicsComponent), new VolumeGraphicsComponent(new Rectangle()));

                    enemy = new Entity2(components);
                    break;
                case EnemyType.Chaser:
                    components = new ComponentsSet(12000.0f);
                    components.AddComponent(typeof(EnemyPhysicsComponent), new EnemyPhysicsComponent(position, velocity, size, 10));
                    components.AddComponent(typeof(SpriteComponent), new SpriteComponent("sprEnemy1"));
                    components.AddComponent(typeof(GraphicsComponent), new GraphicsComponent("sprEnemy1", position, size));
                    //components.AddComponent(typeof(VolumeGraphicsComponent), new VolumeGraphicsComponent(new Rectangle()));
                    components.AddComponent(typeof(ChasingBehaviorComponent), new ChasingBehaviorComponent());

                    enemy = new Entity2(components);
                    break;
                case EnemyType.Carrier:
                    components = new ComponentsSet(12000.0f);
                    components.AddComponent(typeof(EnemyPhysicsComponent), new EnemyPhysicsComponent(position, velocity, size, 5));
                    components.AddComponent(typeof(SpriteComponent), new SpriteComponent("sprEnemy2"));
                    components.AddComponent(typeof(GraphicsComponent), new GraphicsComponent("sprEnemy2", position, size));
                    //components.AddComponent(typeof(VolumeGraphicsComponent), new VolumeGraphicsComponent(new Rectangle()));

                    enemy = new Entity2(components);
                    break;
            }

            return enemy;
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
            float scale = ChooseScale();
            Vector2 size = new Vector2(16.0f, 16.0f) * scale;

            return size;
        }

        private static string ChooseTextureName()
        {
            string textureName;
            int choice = RandomGenerator.Instance.GetRandomInt(1, 10);
            if (choice <= 3)
            {
                textureName = "sprEnemy0";
            }
            else if (choice >= 5)
            {
                textureName = "sprEnemy1";
            }
            else
            {
                textureName = "sprEnemy2";
            }

            return textureName;
        }

        private static float ChooseScale()
        {
            float scale = RandomGenerator.Instance.GetRandomFloat(1.0f, 3.0f);

            return scale;
        }

        private enum EnemyType
        {
            Gunship,
            Chaser,
            Carrier
        }
    }
}