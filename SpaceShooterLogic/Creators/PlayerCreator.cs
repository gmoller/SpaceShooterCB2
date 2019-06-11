using Microsoft.Xna.Framework;
using GameEngineCore;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class PlayerCreator
    {
        public static void Create(GameState state, int score, byte lives)
        {
            Vector2 size = new Vector2(16.0f, 16.0f);

            int entityId = state.EntityCount;

            state.Transforms[entityId] = new Transform(new Vector2(50.0f, 600.0f), 0.0f, new Vector2(2.5f), size);
            state.Velocities[entityId] = new Vector2(0.0f, 0.0f);
            state.Volumes[entityId] = new Rectangle((int)(50.0f - 8.0f), (int)(600.0f - 8.0f), 32, 32);
            state.Textures[entityId] = AssetsManager.Instance.GetTexture("sprPlayer");
            state.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations("sprPlayer"));
            state.Players[entityId] = new Player(score, lives);
            state.Weapons[entityId] = new Weapon();
            state.Tags[entityId] = state.Tags[entityId].SetBits((int)Tag.IsAlive);

            state.EntityCount++;
            state.AliveEntityCount++;
        }
    }
}