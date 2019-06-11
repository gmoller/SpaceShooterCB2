using GameEngineCore;

namespace SpaceShooterLogic.Systems
{
    public class ProjectileCollisionDetectionSystem : System
    {
        public ProjectileCollisionDetectionSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var volume = GameState.GameData.Volumes[entityId];
            var isProjectileTag = GameState.GameData.Tags[entityId].IsBitSet((int)Tag.IsProjectile);

            // selection
            if (!isProjectileTag || volume == null) return;

            // process data
            var collidedWithEntity = -1;
            for (int i = 0; i < GameState.EntityCount - 1; ++i)
            {
                if (entityId == i) continue; // do not check with self
                var isAlive = GameState.GameData.Tags[i].IsBitSet((int)Tag.IsAlive);
                if (!isAlive) continue;

                var volume2 = GameState.GameData.Volumes[i];
                if (volume2 == null) continue;

                var enemy = GameState.GameData.Enemies[i];
                if (enemy != null && volume.Value.Intersects(volume2.Value))
                {
                    collidedWithEntity = i;
                    break;
                }
            }

            // update data
            if (collidedWithEntity >= 0)
            {
                GameState.GameData.Tags[collidedWithEntity] = GameState.GameData.Tags[collidedWithEntity].SetBit((int)Tag.CollisionDetected);
            }
        }
    }
}