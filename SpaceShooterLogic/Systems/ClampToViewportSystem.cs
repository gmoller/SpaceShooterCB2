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
            var size = GameState.Sizes[entityId];
            var tag = GameState.Tags[entityId].IsBitSet(2); // 2-clamp to viewport

            #endregion

            if (!tag || position == null || size == null) return;

            #region process data

            // do not allow our entity off the screen
            var pos = position.Value;
            var sz = size.Value;
            var x = sz.X / 2.0f;
            var y = sz.Y / 2.0f;
            var newPosition = new Vector2(
                MathHelper.Clamp(pos.X, _viewport.Left + x, _viewport.Right - x),
                MathHelper.Clamp(pos.Y, _viewport.Top + y, _viewport.Bottom - y));

            #endregion

            #region update data

            if (pos != newPosition)
            {
                GameState.Positions[entityId] = newPosition;
            }

            #endregion
        }
    }
}