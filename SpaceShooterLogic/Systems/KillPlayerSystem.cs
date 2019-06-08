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
            var player = GameState.Players[entityId];
            var collisionDetectedTag = GameState.Tags[entityId].IsBitSet((int)Tag.CollisionDetected);

            // selection
            if (player.IsNull() || !collisionDetectedTag) return;

            // process data
            GameState.PlayerIsDead = true;
            player.Lives--;

            // update data
            GameState.Players[entityId] = player;
        }
    }
}