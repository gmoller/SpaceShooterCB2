using Microsoft.Xna.Framework;
using GameEngineCore;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class ExplosionCreator
    {
        public static void Create(string textureName, Vector2 position, Vector2 size, GameState state)
        {
            int entityId = state.EntityCount;

            state.Positions[entityId] = position;
            state.Textures[entityId] = AssetsManager.Instance.GetTexture(textureName);
            state.Sizes[entityId] = size;
            state.EnemySpawner[entityId] = new EnemySpawner(-0.1f);
            state.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations(textureName), 0, 0.0f);
            state.Tags[entityId] = state.Tags[entityId].SetBits((int)Tag.IsAlive);

            state.EntityCount++;

            int i = RandomGenerator.Instance.GetRandomInt(0, 1);
            var sound = AssetsManager.Instance.GetSound($"sndExplode{i}");
            state.AddToSoundEffectList(sound);
            state.AliveEntityCount++;
        }
    }
}