using Microsoft.Xna.Framework;
using GameEngineCore;
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
            var isPlayerTag = GameState.Tags[entityId].IsBitSet((int)Tag.IsPlayer);
            var position = GameState.Positions[entityId];
            var velocity = GameState.Velocities[entityId];
            var size = GameState.Sizes[entityId];

            // selection
            if (!isPlayerTag || position.IsNull() || velocity.IsNull() || size.IsNull()) return;

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