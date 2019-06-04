using Microsoft.Xna.Framework;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class ClampToViewportSystem : System
    {
        private readonly Rectangle _viewport;

        public ClampToViewportSystem(string name, GameState gameState) : base(name, gameState)
        {
            _viewport = DeviceManager.Instance.Viewport;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            #region gather data

            var position = GameState.Positions[entityId];
            var velocity = GameState.Velocities[entityId];
            var size = GameState.Sizes[entityId];
            var tag = GameState.Tags[entityId].IsBitSet(2); // 2-clamp to viewport

            #endregion

            if (!tag || position.IsNull() || velocity.IsNull() || size.IsNull()) return;

            #region process data

            // do not allow our entity off the screen
            var x = size.X / 2.0f;
            var y = size.Y / 2.0f;
            var newPosition = new Vector2(
                MathHelper.Clamp(position.X, _viewport.Left + x, _viewport.Right - x),
                MathHelper.Clamp(position.Y, _viewport.Top + y, _viewport.Bottom - y));

            #endregion

            #region update data

            if (position != newPosition)
            {
                GameState.Positions[entityId] = newPosition;
            }

            #endregion
        }
    }
}