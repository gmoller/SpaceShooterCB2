using System.CodeDom;
using GameEngineCore;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class PlayerCreator
    {
        public static Entity Create(UpdateComponent inputComponent, GameState state)
        {
            Vector2 size = new Vector2(16.0f, 16.0f) * 2.5f;

            var components = new ComponentsSet();
            components.AddComponent(typeof(PlayerInputComponent), inputComponent);
            components.AddComponent(typeof(PlayerPhysicsComponent), new PlayerPhysicsComponent(DeviceManager.Instance.ScreenDimensions * 0.5f, size));
            components.AddComponent(typeof(LaserComponent), new LaserComponent(new Vector2(0.0f, -1.0f), new Vector2(0.0f, -size.Y), 0.600f, 200));
            components.AddComponent(typeof(SpriteComponent), new SpriteComponent("sprPlayer"));
            components.AddComponent(typeof(GraphicsComponent), new GraphicsComponent("sprPlayer", Vector2.Zero, size));
            //components.AddComponent(typeof(VolumeGraphicsComponent), new VolumeGraphicsComponent(new Rectangle()));

            var player = new Entity(components);

            // new:
            //Registrar.Instance.EntityCount++; // temporary create an empty entity - for testing

            for (int i = 0; i < 1; ++i)
            {
                state.Tags[Registrar.Instance.EntityCount] = state.Tags[Registrar.Instance.EntityCount].SetBit(0); // 0=playerinput
                state.Velocities[Registrar.Instance.EntityCount] = new Vector2(0.0f, 0.0f);
                state.Positions[Registrar.Instance.EntityCount] = new Vector2(50.0f, 600.0f);
                state.Volumes[Registrar.Instance.EntityCount] = new Rectangle(0, 0, 16, 16);
                state.Textures[Registrar.Instance.EntityCount] = AssetsManager.Instance.GetTexture("sprPlayer");
                state.Sizes[Registrar.Instance.EntityCount] = size;
                state.Frames[Registrar.Instance.EntityCount] = new Rectangle(0, 0, 16, 16);
                state.Rotations[Registrar.Instance.EntityCount] = 0.0f;

                Registrar.Instance.EntityCount++;
            }

            return player;
        }
    }
}