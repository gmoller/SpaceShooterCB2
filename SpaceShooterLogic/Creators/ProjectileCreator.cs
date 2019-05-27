using GameEngineCore;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Creators
{
    public static class ProjectileCreator
    {
        public static Entity2 Create(Vector2 position, Vector2 velocity)
        {
            var components = new ComponentsSet(1000.0f);
            components.AddComponent(ComponentType.Physics, new ProjectilePhysicsComponent(position, velocity, new Vector2(1.0f, 8.0f)));
            components.AddComponent(ComponentType.Graphics, new GraphicsComponent("sprLaserPlayer", position));
            components.AddComponent(ComponentType.VolumeGraphics, new VolumeGraphicsComponent(new Rectangle()));

            var projectile = new Entity2(components);

            return projectile;
        }
    }
}