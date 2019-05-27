using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

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

        public static int Create(UpdateComponent inputComponent)
        {
            var components = new ComponentsSet();
            components.AddComponent(ComponentType.Input, inputComponent);
            components.AddComponent(ComponentType.Physics, new PlayerPhysicsComponent(DeviceManager.Instance.ScreenDimensions * 0.5f));
            components.AddComponent(ComponentType.Laser, new PlayerLaserComponent());
            components.AddComponent(ComponentType.Sprite, new SpriteComponent("sprPlayer"));
            components.AddComponent(ComponentType.Graphics, new GraphicsComponent("sprPlayer", Vector2.Zero));
            components.AddComponent(ComponentType.VolumeGraphics, new VolumeGraphicsComponent(new Rectangle()));

            var player = new Player(components);

            return components.EntityId;
        }
    }
}