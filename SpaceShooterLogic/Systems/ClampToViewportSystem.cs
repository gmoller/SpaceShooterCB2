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
            // gather data for selection
            var player = GameState.Players[entityId];
            var position = GameState.Positions[entityId];
            var size = GameState.Sizes[entityId];

            // selection
            if (player.IsNull() || position.IsNull() || size.IsNull()) return;

            // process data
            // do not allow our entity off the screen
            var x = size.X / 2.0f;
            var y = size.Y / 2.0f;
            var newPosition = new Vector2(
                MathHelper.Clamp(position.X, _viewport.Left + x, _viewport.Right - x),
                MathHelper.Clamp(position.Y, _viewport.Top + y, _viewport.Bottom - y));

            // update data
            if (position != newPosition)
            {
                GameState.Positions[entityId] = newPosition;
            }
        }
    }
}