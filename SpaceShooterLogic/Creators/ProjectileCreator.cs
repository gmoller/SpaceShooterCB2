using GameEngineCore;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class ProjectileCreator
    {
        public static Entity Create(string textureName, Vector2 position, Vector2 velocity)
        {
            Vector2 size = new Vector2(1.0f, 8.0f);

            var components = new ComponentsSet(2000.0f);
            components.AddComponent(typeof(ProjectilePhysicsComponent), new ProjectilePhysicsComponent(position, velocity, size));
            components.AddComponent(typeof(GraphicsComponent), new GraphicsComponent(textureName, position, size));
            //components.AddComponent(typeof(VolumeGraphicsComponent), new VolumeGraphicsComponent(new Rectangle()));

            var projectile = new Entity(components);

            return projectile;
        }

        public static void Create2(string textureName, Vector2 projectilePosition, Vector2 projectileVelocity, GameState state)
        {
            Vector2 size = new Vector2(1.0f, 8.0f);

            state.Tags[Registrar.Instance.EntityCount] = 0;
            state.Velocities[Registrar.Instance.EntityCount] = projectileVelocity;
            state.Positions[Registrar.Instance.EntityCount] = projectilePosition;
            state.Volumes[Registrar.Instance.EntityCount] = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            state.Textures[Registrar.Instance.EntityCount] = AssetsManager.Instance.GetTexture(textureName);
            state.Sizes[Registrar.Instance.EntityCount] = size;
            state.Frames[Registrar.Instance.EntityCount] = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            state.Rotations[Registrar.Instance.EntityCount] = 0.0f;

            Registrar.Instance.EntityCount++;
        }
    }
}