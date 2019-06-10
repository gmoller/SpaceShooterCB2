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
            var transform = GameState.Transforms[entityId];
            var enemy = GameState.Enemies[entityId];
            var isProjectileTag = GameState.Tags[entityId].IsBitSet((int)Tag.IsProjectile);

            // selection
            if (transform == null || (enemy == null && !isProjectileTag)) return;

            // process data
            // destroy if off screen
            var t = transform.Value;
            var x = t.Size.X / 2.0f;
            var y = t.Size.Y / 2.0f;
            bool destroy = !_viewport.Intersects(new Rectangle((int)(t.Position.X - x),(int)(t.Position.Y - y),(int)t.Size.X, (int)t.Size.Y));

            // update data
            if (destroy)
            {
                GameState.Tags[entityId] = 0; // destroy
            }
        }
    }
}