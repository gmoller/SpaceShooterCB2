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
            var tag1 = GameState.Tags[entityId].IsBitSet((int)Tag.PlayerInput);
            var tag5 = GameState.Tags[entityId].IsBitSet((int)Tag.CollisionDetected);

            // selection
            if (!tag1 || !tag5) return;

            // process data
            GameState.PlayerIsDead = true;
        }
    }
}