using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Creators
{
    public static class ExplosionCreator
    {
        public static Explosion Create(Vector2 position, Vector2 size)
        {
            var components = new ComponentsSet(500.0f);
            components.AddComponent(ComponentType.Sprite, new SpriteComponent("Fireball02"));
            components.AddComponent(ComponentType.Graphics, new GraphicsComponent("Fireball02", position));
            components.AddComponent(ComponentType.VolumeGraphics, new VolumeGraphicsComponent(new Rectangle((int)(position.X - 64.0f), (int)(position.Y - 64.0f), 128, 128)));

            var explosion = new Explosion(components);

            return explosion;
        }
    }
}