using SpaceShooterLogic.Components;

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
            var transform = GameState.Transforms[entityId];
            var velocity = GameState.Velocities[entityId];

            // selection
            if (transform == null|| velocity == null) return;

            // process data
            // calculate new position
            var t = transform.Value;
            var newPosition = t.Position + velocity.Value * deltaTime;

            // update data
            GameState.Transforms[entityId] = new Transform(newPosition, t.Rotation, t.Scale, t.Size);
        }
    }
}