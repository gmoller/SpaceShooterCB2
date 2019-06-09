using GameEngineCore;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Systems
{
    public class PlayerCollisionDetectionSystem : System
    {
        public PlayerCollisionDetectionSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var volume = GameState.Volumes[entityId];
            var player = GameState.Players[entityId];
             
            // selection
            if (player.IsNull() || player.Status == PlayerStatus.Destroyed || volume.IsEmpty) return;

            // process data
            for (int i = 0; i < GameState.EntityCount - 1; ++i)
            {
                if (entityId == i) continue; // do not check with self
                var isAliveTag = GameState.Tags[i].IsBitSet((int)Tag.IsAlive);
                if (!isAliveTag) continue;

                var volume2 = GameState.Volumes[i];
                if (volume2.IsEmpty) continue;

                // collision with enemy:
                var enemy = GameState.Enemies[i];
                if (!enemy.IsNull() && volume.Intersects(volume2))
                {
                    player.Status = PlayerStatus.Destroyed;
                    break;
                }

                // collision with projectile:
                var isProjectileTag = GameState.Tags[i].IsBitSet((int)Tag.IsProjectile);
                if (isProjectileTag && volume.Intersects(volume2))
                {
                    player.Status = PlayerStatus.Destroyed;
                    break;
                }
            }

            // update data
            if (player.Status == PlayerStatus.Destroyed)
            {
                player.DeathCooldownTime = 3000.0f; // 3 seconds
                GameState.Players[entityId] = player;
            }
        }
    }
}