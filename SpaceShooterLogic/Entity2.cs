using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic
{
    public class Entity2
    {
        private readonly ComponentsSet _components;

        internal Entity2(ComponentsSet components)
        {
            _components = components;
        }

        public void Update(GameTime gameTime)
        {
            if (!float.IsPositiveInfinity(_components.LifeTime))
            {
                _components.LifeTime -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (_components.LifeTime <= 0)
                {
                    _components.IsDeleted = true;
                }
            }

            foreach (IComponent component in _components)
            {
                if (component is UpdateComponent)
                {
                    UpdateComponent uc = (UpdateComponent) component;
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
    }
}