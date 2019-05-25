using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic
{
    public class Player
    {
        private readonly ComponentsSet _components;

        internal Player(ComponentsSet components)
        {
            _components = components;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Component component in _components)
            {
                component.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var graphicsComponent = (GraphicsComponent)_components[ComponentType.Graphics];
            graphicsComponent.Draw(spriteBatch);
        }
    }
}