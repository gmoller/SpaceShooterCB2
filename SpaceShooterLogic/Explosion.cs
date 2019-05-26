using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic
{
    public class Explosion
    {
        private readonly ComponentsSet _components;

        internal Explosion(ComponentsSet components)
        {
            _components = components;
        }

        public void Update(GameTime gameTime)
        {
            foreach (IComponent component in _components)
            {
                if (component is UpdateComponent)
                {
                    UpdateComponent uc = (UpdateComponent)component;
                    uc.Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var graphicsComponent = (GraphicsComponent)_components[ComponentType.Graphics];
            graphicsComponent.Draw(spriteBatch);

            var volumeGraphicsComponent = (VolumeGraphicsComponent)_components[ComponentType.VolumeGraphics];
            volumeGraphicsComponent.Draw(spriteBatch);
        }

        public bool IsFinished
        {
            get
            {
                return false; // TODO: implement this properly
            }
        }

        public static Explosion Create(Vector2 position, Vector2 size)
        {
            var components = new ComponentsSet();
            components.AddUpdateComponent(ComponentType.Sprite, new SpriteComponent("Fireball02"));
            components.AddDrawComponent(ComponentType.Graphics, new GraphicsComponent("Fireball02", position));
            components.AddDrawComponent(ComponentType.VolumeGraphics, new VolumeGraphicsComponent(new Rectangle((int)(position.X - 64.0f), (int)(position.Y - 64.0f), 128, 128)));

            var explosion = new Explosion(components);

            return explosion;
            //return components.EntityId;
        }
    }

    public class Explosions
    {
        private readonly List<Explosion> _explosions = new List<Explosion>();

        public void Add(Explosion explosion)
        {
            _explosions.Add(explosion);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _explosions.Count; i++)
            {
                var explosion = _explosions[i];
                explosion.Update(gameTime);
                if (explosion.IsFinished)
                {
                    _explosions.Remove(explosion);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Explosion explosion in _explosions)
            {
                explosion.Draw(spriteBatch);
            }
        }
    }
}