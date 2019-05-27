using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Creators
{
    public static class EnemyCreator
    {
        public static Entity2 Create(Vector2 position, Vector2 velocity)
        {
            var components = new ComponentsSet();
            components.AddComponent(ComponentType.Physics, new ProjectilePhysicsComponent(position, velocity, new Vector2(16.0f, 16.0f)));
            components.AddComponent(ComponentType.Sprite, new SpriteComponent("sprEnemy0"));
            components.AddComponent(ComponentType.Graphics, new GraphicsComponent("sprEnemy0", position));
            components.AddComponent(ComponentType.VolumeGraphics, new VolumeGraphicsComponent(new Rectangle()));

            var enemy = new Entity2(components);

            return enemy;
        }
    }
}