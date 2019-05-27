using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Creators
{
    public static class ProjectileCreator
    {
        public static Explosion Create(Vector2 position, Vector2 velocity)
        {
            var components = new ComponentsSet(1000.0f);
            components.AddComponent(ComponentType.Physics, new ProjectilePhysicsComponent(position, velocity));
            components.AddComponent(ComponentType.Graphics, new GraphicsComponent("sprLaserPlayer", position));
            components.AddComponent(ComponentType.VolumeGraphics, new VolumeGraphicsComponent(new Rectangle((int)position.X, (int)position.Y, 1, 8)));

            var projectile = new Explosion(components);

            return projectile;
        }
    }
}