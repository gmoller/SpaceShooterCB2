using Microsoft.Xna.Framework;
using GameEngineCore;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class PlayerCreator
    {
        public static void Create(GameState state)
        {
            Vector2 size = new Vector2(16.0f, 16.0f) * 2.5f;

            int entityId = state.EntityCount;

            state.Positions[entityId] = new Vector2(50.0f, 600.0f);
            state.Velocities[entityId] = new Vector2(0.0f, 0.0f);
            state.Volumes[entityId] = new Rectangle((int)(50.0f - 8.0f), (int)(600.0f - 8.0f), 16, 16);
            state.Textures[entityId] = AssetsManager.Instance.GetTexture("sprPlayer");
            state.Sizes[entityId] = size;
            state.TimesSinceLastShot[entityId] = float.MaxValue; // to ensure we don't start on cooldown
            state.TimesSinceLastEnemySpawned[entityId] = -0.1f;
            state.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations("sprPlayer"), 0, 0.0f);
            state.Tags[entityId] = state.Tags[entityId].SetBits((int)Tag.IsAlive, (int)Tag.PlayerInput, (int)Tag.ClampToViewport, (int)Tag.IsPlayer);

            state.EntityCount++;
        }
    }
}