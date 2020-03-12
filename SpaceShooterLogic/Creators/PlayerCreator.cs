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
            var size = new Vector2(16.0f, 16.0f);
            var startPos = new Vector2(DeviceManager.Instance.ScreenWidth / 2.0f, DeviceManager.Instance.ScreenHeight - 40.0f);

            var entityId = state.EntityCount;

            state.GameData.Transforms[entityId] = new Transform(new Vector2(startPos.X, startPos.Y), Color.White, 0.0f, new Vector2(2.5f), size);
            state.GameData.Velocities[entityId] = new Vector2(0.0f, 0.0f);
            state.GameData.Volumes[entityId] = new Rectangle((int)(startPos.X - size.X / 2.0f), (int)(startPos.Y - size.Y / 2.0f), (int)(size.X * 2), (int)(size.Y * 2));
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