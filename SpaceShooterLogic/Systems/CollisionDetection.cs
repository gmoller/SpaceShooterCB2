using GameEngineCore;

namespace SpaceShooterLogic.Systems
{
    public class CollisionDetection : System
    {
        public CollisionDetection(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var volume = GameState.Volumes[entityId];

            // selection
            if (volume.IsEmpty) return;

            // process data
            int collidedWithEntity = -1;
            for (int i = 0; i < GameState.EntityCount-1; ++i)
            {
                if (entityId == i) continue; // do not check with self
                var isAlive = GameState.Tags[i].IsBitSet((int)Tag.IsAlive);
                if (!isAlive) continue;

                var volume2 = GameState.Volumes[i];
                if (volume2.IsEmpty) continue;
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
}