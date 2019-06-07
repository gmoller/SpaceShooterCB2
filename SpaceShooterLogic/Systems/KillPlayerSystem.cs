using GameEngineCore;

namespace SpaceShooterLogic.Systems
{
    public class KillPlayerSystem : System
    {
        public KillPlayerSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var isPlayerTag = GameState.Tags[entityId].IsBitSet((int)Tag.IsPlayer);
            var collisionDetectedTag = GameState.Tags[entityId].IsBitSet((int)Tag.CollisionDetected);

            // selection
            if (!isPlayerTag || !collisionDetectedTag) return;

            // process data
            GameState.PlayerIsDead = true;
        }
    }
}