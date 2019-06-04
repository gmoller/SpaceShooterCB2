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
            #region gather data

            var position = GameState.Positions[entityId];
            var velocity = GameState.Velocities[entityId];

            #endregion

            if (position.IsNull() || velocity.IsNull()) return;

            #region process data

            // calculate new position
            var newPosition = position + velocity * deltaTime;

            #endregion

            #region update data

            GameState.Positions[entityId] = newPosition;

            #endregion
        }
    }
}