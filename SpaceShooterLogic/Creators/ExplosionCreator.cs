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
            int entityId = state.EntityCount;

            float scaleX = size.X * scale / 256.0f;
            float scaleY = size.Y * scale / 256.0f;
            state.Transforms[entityId] = new Transform(position, 0.0f, new Vector2(scaleX, scaleY), new Vector2(256.0f, 256.0f));
            state.Textures[entityId] = AssetsManager.Instance.GetTexture(textureName);
            state.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations(textureName));
            state.Tags[entityId] = state.Tags[entityId].SetBits((int)Tag.IsAlive);

            state.EntityCount++;

            int i = RandomGenerator.Instance.GetRandomInt(0, 1);
            var sound = AssetsManager.Instance.GetSound($"sndExplode{i}");
            state.AddToSoundEffectList(sound);
            state.AliveEntityCount++;
        }
    }
}