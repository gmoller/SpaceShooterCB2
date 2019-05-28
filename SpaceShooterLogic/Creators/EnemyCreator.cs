using GameEngineCore;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Creators
{
    public static class EnemyCreator
    {
        public static Entity2 Create(Vector2 position, Vector2 velocity)
        {
            var components = new ComponentsSet();
            components.AddComponent(typeof(PhysicsComponent), new PhysicsComponent(position, velocity, new Vector2(16.0f, 16.0f)));
            components.AddComponent(typeof(SpriteComponent), new SpriteComponent("sprEnemy0"));
            components.AddComponent(typeof(GraphicsComponent), new GraphicsComponent("sprEnemy0", position));
            components.AddComponent(typeof(VolumeGraphicsComponent), new VolumeGraphicsComponent(new Rectangle()));

            var enemy = new Entity2(components);

            return enemy;
        }
    }
}