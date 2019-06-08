using GameEngineCore;

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
            if (player.IsNull() || volume.IsEmpty) return;

            // process data
            var collidedWithEntity = -1;
            for (int i = 0; i < GameState.EntityCount - 1; ++i)
            {
                if (entityId == i) continue; // do not check with self
                var isAliveTag = GameState.Tags[i].IsBitSet((int)Tag.IsAlive);
                if (!isAliveTag) continue;

                var volume2 = GameState.Volumes[i];
                if (volume2.IsEmpty) continue;

                var enemy = GameState.Enemies[i];
                if (enemy.IsNull()) continue;
                if (volume.Intersects(volume2))
                {
                    collidedWithEntity = i;
                    break;
                }
            }

            // update data
            if (collidedWithEntity >= 0)
            {
                GameState.Tags[entityId] = GameState.Tags[entityId].SetBit((int)Tag.CollisionDetected);
                GameState.Tags[collidedWithEntity] = GameState.Tags[collidedWithEntity].SetBit((int)Tag.CollisionDetected);
            }
        }
    }

    public class ProjectileCollisionDetectionSystem : System
    {
        public ProjectileCollisionDetectionSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var volume = GameState.Volumes[entityId];
            var isProjectileTag = GameState.Tags[entityId].IsBitSet((int)Tag.IsProjectile);

            // selection
            if (!isProjectileTag || volume.IsEmpty) return;

            // process data
            var collidedWithEntity = -1;
            for (int i = 0; i < GameState.EntityCount - 1; ++i)
            {
                if (entityId == i) continue; // do not check with self
                var isAlive = GameState.Tags[i].IsBitSet((int)Tag.IsAlive);
                if (!isAlive) continue;

                var volume2 = GameState.Volumes[i];
                if (volume2.IsEmpty) continue;

                var enemy = GameState.Enemies[i];
                if (!enemy.IsNull() && volume.Intersects(volume2))
                {
                    collidedWithEntity = i;
                    break;
                }

                var player = GameState.Players[i];
                if (!player.IsNull() && volume.Intersects(volume2))
                {
                    collidedWithEntity = i;
                    break;
                }
            }

            // update data
            if (collidedWithEntity >= 0)
            {
                //GameState.Tags[entityId] = GameState.Tags[entityId].SetBit((int)Tag.CollisionDetected);
                GameState.Tags[collidedWithEntity] = GameState.Tags[collidedWithEntity].SetBit((int)Tag.CollisionDetected);
            }
        }
    }
}