using Microsoft.Xna.Framework;
using GameEngineCore;
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
            // gather data for selection
            var enemy = GameState.Enemies[entityId];
            var isProjectileTag = GameState.Tags[entityId].IsBitSet((int)Tag.IsProjectile);
            var position = GameState.Positions[entityId];
            var velocity = GameState.Velocities[entityId];
            var size = GameState.Sizes[entityId];

            // selection
            if ((enemy.IsNull() && !isProjectileTag) || position.IsNull() || velocity.IsNull() || size.IsNull()) return;

            // process data
            // destroy if off screen
            var x = size.X / 2.0f;
            var y = size.Y / 2.0f;
            bool destroy = !_viewport.Intersects(new Rectangle((int)(position.X - x),(int)(position.Y - y),(int)size.X,(int)size.Y));

            // update data
            if (destroy)
            {
                GameState.Tags[entityId] = 0; // destroy
            }
        }
    }
}