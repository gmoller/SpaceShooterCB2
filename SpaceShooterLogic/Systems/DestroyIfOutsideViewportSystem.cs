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
            var size = GameState.Sizes[entityId];
            var tag = GameState.Tags[entityId].IsBitSet(3); // 3-destroy if outside viewport

            #endregion

            if (!tag || position == null || size == null) return;

            #region process data

            // destroy if off screen
            var pos = position.Value;
            var sz = size.Value;
            var x = sz.X / 2.0f;
            var y = sz.Y / 2.0f;
            bool destroy = !_viewport.Intersects(new Rectangle((int)(pos.X - x),(int)(pos.Y - y),(int)sz.X,(int)sz.Y));

            #endregion

            #region update data

            if (destroy)
            {
                GameState.Positions[entityId] = null;
                GameState.Velocities[entityId] = null;
                GameState.Volumes[entityId] = null;
                GameState.Textures[entityId] = null;
                GameState.Sizes[entityId] = null;
                GameState.Rotations[entityId] = null;
                GameState.TimesSinceLastShot[entityId] = null;
                GameState.AnimationData[entityId] = null;
                GameState.Tags[entityId] = 0;
            }

            #endregion
        }
    }
}