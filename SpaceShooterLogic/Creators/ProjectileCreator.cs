using GameEngineCore;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Creators
{
    public static class ProjectileCreator
    {
        public static Entity2 Create(string textureName, Vector2 position, Vector2 velocity)
        {
            Vector2 size = new Vector2(1.0f, 8.0f);

            var components = new ComponentsSet(2000.0f);
            components.AddComponent(typeof(ProjectilePhysicsComponent), new ProjectilePhysicsComponent(position, velocity, size));
            components.AddComponent(typeof(GraphicsComponent), new GraphicsComponent(textureName, position, size));
            //components.AddComponent(typeof(VolumeGraphicsComponent), new VolumeGraphicsComponent(new Rectangle()));

            var projectile = new Entity2(components);

            return projectile;
        }
    }
}