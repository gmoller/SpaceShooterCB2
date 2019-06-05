using Microsoft.Xna.Framework;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class ExplosionCreator
    {
        public static void Create2(string textureName, Vector2 position, Vector2 size, GameState state)
        {
            int entityId = state.EntityCount;

            state.Positions[entityId] = position;
            state.Velocities[entityId] = Vector2.Zero;
            state.Volumes[entityId] = Rectangle.Empty;
            state.Textures[entityId] = AssetsManager.Instance.GetTexture(textureName);
            state.Sizes[entityId] = size;
            state.Rotations[entityId] = 0.0f;
            state.TimesSinceLastShot[entityId] = -0.1f;
            state.TimesSinceLastEnemySpawned[entityId] = -0.1f;
            state.AnimationData[entityId] = new AnimationData(AssetsManager.Instance.GetAnimations(textureName), 0, 0.0f);
            state.Tags[entityId] = state.Tags[entityId].SetBits((int)Tag.IsAlive, (int)Tag.DestroyIfOutsideViewport); // 17

            state.EntityCount++;
        }
    }
}