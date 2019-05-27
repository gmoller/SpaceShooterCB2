using GameEngineCore;
using GameEngineCore.AbstractClasses;
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
            components.AddComponent(typeof(PlayerInputComponent), inputComponent);
            components.AddComponent(typeof(PlayerPhysicsComponent), new PlayerPhysicsComponent(DeviceManager.Instance.ScreenDimensions * 0.5f));
            components.AddComponent(typeof(PlayerLaserComponent), new PlayerLaserComponent());
            components.AddComponent(typeof(SpriteComponent), new SpriteComponent("sprPlayer"));
            components.AddComponent(typeof(GraphicsComponent), new GraphicsComponent("sprPlayer", Vector2.Zero));
            components.AddComponent(typeof(VolumeGraphicsComponent), new VolumeGraphicsComponent(new Rectangle()));

            var player = new Entity2(components);

            return player;
        }
    }
}