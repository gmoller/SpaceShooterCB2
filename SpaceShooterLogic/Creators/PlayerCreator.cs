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

            {
                for (int i = 0; i < 1; ++i)
                {
                    int entityId = Registrar.Instance.EntityCount;

                    state.Positions[entityId] = new Vector2(50.0f, 600.0f);
                    state.Velocities[entityId] = new Vector2(0.0f, 0.0f);
                    state.Volumes[entityId] = new Rectangle(0, 0, 16, 16);
                    state.Textures[entityId] = AssetsManager.Instance.GetTexture("sprPlayer");
                    state.Sizes[entityId] = size;
                    state.Rotations[entityId] = 0.0f;
                    state.TimesSinceLastShot[entityId] = float.MaxValue; // to ensure we don't start on cooldown
                    state.TimesSinceLastEnemySpawned[entityId] = -0.1f;
                    state.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations("sprPlayer"), 0, 0.0f);
                    state.Tags[entityId] = state.Tags[entityId].SetBits(0, 2); // 0=player input, 2=clamp to viewport

                    Registrar.Instance.EntityCount++;
                }
            }

            return player;
        }
    }
}