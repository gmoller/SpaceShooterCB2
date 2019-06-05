using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Creators
{
    public static class SpawnCreator
    {
        public static void Create(GameState state)
        {
            int entityId = state.EntityCount;

            state.Positions[entityId] = Vector2.Zero;
            state.Velocities[entityId] = Vector2.Zero;
            state.Volumes[entityId] = Rectangle.Empty;
            state.Textures[entityId] = null;
            state.Sizes[entityId] = Vector2.Zero;
            state.Rotations[entityId] = 0.0f;
            state.TimesSinceLastShot[entityId] = -0.1f;
            state.TimesSinceLastEnemySpawned[entityId] = float.MaxValue;
            state.AnimationData[entityId] = new AnimationData(null, 0, 0.0f);
            state.Tags[entityId] = 1;

            state.EntityCount++;
        }
    }
}