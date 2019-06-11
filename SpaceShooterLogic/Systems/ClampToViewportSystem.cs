using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;
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
            var transform = GameState.GameData.Transforms[entityId];
            var player = GameState.GameData.Players[entityId];

            // selection
            if (transform == null || player == null) return;

            // process data
            // do not allow our entity off the screen
            var t = transform.Value;
            var x = t.Size.X * t.Scale.X / 2.0f;
            var y = t.Size.Y * t.Scale.Y / 2.0f;
            var newPosition = new Vector2(
                MathHelper.Clamp(t.Position.X, _viewport.Left + x, _viewport.Right - x),
                MathHelper.Clamp(t.Position.Y, _viewport.Top + y, _viewport.Bottom - y));

            // update data
            if (t.Position != newPosition)
            {
                GameState.GameData.Transforms[entityId] = new Transform(newPosition, t.Rotation, t.Scale, t.Size);
            }
        }
    }
}