using Microsoft.Xna.Framework;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class DestroyIfOutsideViewportSystem : System
    {
        private readonly Rectangle _viewport;

        public DestroyIfOutsideViewportSystem(string name, GameState gameState) : base(name, gameState)
        {
            _viewport = DeviceManager.Instance.Viewport;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            #region gather data

            var position = GameState.Positions[entityId];
            var velocity = GameState.Velocities[entityId];
            var size = GameState.Sizes[entityId];
            var tag = GameState.Tags[entityId].IsBitSet(3); // 3-destroy if outside viewport

            #endregion

            if (!tag || position.IsNull() || velocity.IsNull() || size.IsNull()) return;

            #region process data

            // destroy if off screen
            var x = size.X / 2.0f;
            var y = size.Y / 2.0f;
            bool destroy = !_viewport.Intersects(new Rectangle((int)(position.X - x),(int)(position.Y - y),(int)size.X,(int)size.Y));

            #endregion

            #region update data

            if (destroy)
            {
                GameState.Positions[entityId] = Vector2.Zero;
                GameState.Velocities[entityId] = Vector2.Zero;
                GameState.Volumes[entityId] = Rectangle.Empty;
                GameState.Textures[entityId] = null;
                GameState.Sizes[entityId] = Vector2.Zero;
                GameState.Rotations[entityId] = 0.0f;
                GameState.TimesSinceLastShot[entityId] = -0.1f;
                GameState.TimesSinceLastEnemySpawned[entityId] = -0.1f;
                GameState.AnimationData[entityId] = new AnimationData(null, 0, 0.0f);
                GameState.Tags[entityId] = 0;
            }

            #endregion
        }
    }
}