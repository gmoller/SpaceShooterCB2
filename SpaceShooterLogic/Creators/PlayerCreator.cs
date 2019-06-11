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

            state.GameData.Transforms[entityId] = new Transform(new Vector2(50.0f, 600.0f), 0.0f, new Vector2(2.5f), size);
            state.GameData.Velocities[entityId] = new Vector2(0.0f, 0.0f);
            state.GameData.Volumes[entityId] = new Rectangle((int)(50.0f - 8.0f), (int)(600.0f - 8.0f), 32, 32);
            state.GameData.Textures[entityId] = AssetsManager.Instance.GetTexture("sprPlayer");
            state.GameData.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations("sprPlayer"));
            state.GameData.Players[entityId] = new Player(score, lives);
            state.GameData.Weapons[entityId] = new Weapon();
            state.GameData.Tags[entityId] = state.GameData.Tags[entityId].SetBits((int)Tag.IsAlive);

            state.EntityCount++;
            state.AliveEntityCount++;
        }
    }
}