using GameEngineCore;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Creators
{
    public static class ExplosionCreator
    {
        public static Entity2 Create(Vector2 position, Vector2 size)
        {
            var components = new ComponentsSet(500.0f);
            components.AddComponent(typeof(SpriteComponent), new SpriteComponent("Fireball02"));
            components.AddComponent(typeof(GraphicsComponent), new GraphicsComponent("Fireball02", position));
            components.AddComponent(typeof(VolumeGraphicsComponent), new VolumeGraphicsComponent(new Rectangle((int)(position.X - 64.0f), (int)(position.Y - 64.0f), 128, 128)));

            var explosion = new Entity2(components);

            return explosion;
        }
    }
}