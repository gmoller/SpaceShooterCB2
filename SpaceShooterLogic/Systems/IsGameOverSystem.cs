namespace SpaceShooterLogic.Systems
{
    public class IsGameOverSystem : System
    {
        public IsGameOverSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var player = GameState.GameData.Players[entityId];

            // selection
            if (player == null || player.Value.Lives > 0) return;

            // process data
            GameState.GameOver = true;

            // update data
            // nothing to update
        }
    }
}