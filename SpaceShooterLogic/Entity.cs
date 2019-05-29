using GameEngineCore;
using GameEngineCore.AbstractClasses;
using GameEngineCore.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic
{
    public class Entity
    {
        private readonly ComponentsSet _components;

        internal Entity(ComponentsSet components)
        {
            _components = components;
        }

        public void Update(float deltaTime)
        {
            if (!float.IsPositiveInfinity(_components.LifeTime))
            {
                _components.LifeTime -= deltaTime;
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
                    uc.Update(deltaTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var graphicsComponent = (GraphicsComponent)_components[typeof(GraphicsComponent)];
            graphicsComponent.Draw(spriteBatch);

            if (_components.HasComponent(typeof(VolumeGraphicsComponent)))
            {
                var volumeGraphicsComponent = (VolumeGraphicsComponent) _components[typeof(VolumeGraphicsComponent)];
                volumeGraphicsComponent.Draw(spriteBatch);
            }
        }
    }
}