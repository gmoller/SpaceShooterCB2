using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class MovementSystem : System
    {
        public MovementSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var position = GameState.Positions[entityId];
            var velocity = GameState.Velocities[entityId];

            // selection
            if (position.IsNull() || velocity.IsNull()) return;

            // process data
            // calculate new position
            var newPosition = position + velocity * deltaTime;

            // update data
            GameState.Positions[entityId] = newPosition;
        }
    }
}