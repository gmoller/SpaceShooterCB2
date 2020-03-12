using Microsoft.Xna.Framework;
using GameEngineCore;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class ExplosionCreator
    {
        public static void Create(string textureName, Vector2 position, float scale, Vector2 size, GameState state)
        {
            var entityId = state.EntityCount;

            var scaleX = size.X * scale / 256.0f;
            var scaleY = size.Y * scale / 256.0f;
            state.GameData.Transforms[entityId] = new Transform(position, Color.White, 0.0f, new Vector2(scaleX, scaleY), new Vector2(256.0f, 256.0f));
            state.GameData.Textures[entityId] = AssetsManager.Instance.GetTexture(textureName);
            state.GameData.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations(textureName));
            state.GameData.Tags[entityId] = state.GameData.Tags[entityId].SetBits((int)Tag.IsAlive);

            state.EntityCount++;

            var i = RandomGenerator.Instance.GetRandomInt(0, 1);
            var sound = AssetsManager.Instance.GetSound($"sndExplode{i}");
            state.AddToSoundEffectList(sound);
            state.AliveEntityCount++;
        }
    }
}