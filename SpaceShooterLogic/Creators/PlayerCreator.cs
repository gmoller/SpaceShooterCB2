using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class PlayerCreator
    {
        public static Entity2 Create(UpdateComponent inputComponent)
        {
            var components = new ComponentsSet();
            components.AddComponent(ComponentType.Input, inputComponent);
            components.AddComponent(ComponentType.Physics, new PlayerPhysicsComponent(DeviceManager.Instance.ScreenDimensions * 0.5f));
            components.AddComponent(ComponentType.Laser, new PlayerLaserComponent());
            components.AddComponent(ComponentType.Sprite, new SpriteComponent("sprPlayer"));
            components.AddComponent(ComponentType.Graphics, new GraphicsComponent("sprPlayer", Vector2.Zero));
            components.AddComponent(ComponentType.VolumeGraphics, new VolumeGraphicsComponent(new Rectangle()));

            var player = new Entity2(components);

            return player;
        }
    }
}