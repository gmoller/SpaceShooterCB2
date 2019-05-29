using GameEngineCore;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Creators
{
    public static class ExplosionCreator
    {
        public static Entity Create(string textureName, Vector2 position, Vector2 size)
        {
            var components = new ComponentsSet(500.0f);
            components.AddComponent(typeof(SpriteComponent), new SpriteComponent(textureName));
            components.AddComponent(typeof(GraphicsComponent), new GraphicsComponent(textureName, position, size));
            //components.AddComponent(typeof(VolumeGraphicsComponent), new VolumeGraphicsComponent(new Rectangle((int)(position.X - 64.0f), (int)(position.Y - 64.0f), 128, 128)));

            var explosion = new Entity(components);

            return explosion;
        }
    }
}