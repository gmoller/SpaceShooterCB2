using Microsoft.Xna.Framework;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class SetBoundingBoxSystem : System
    {
        public SetBoundingBoxSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var position = GameState.Positions[entityId];
            var velocity = GameState.Velocities[entityId];
            var volume = GameState.Volumes[entityId];

            // selection
            if (position.IsNull() || velocity.IsNull() || volume.IsEmpty) return;

            // process data
            // calculate new Bounding Box
            var origin = new Vector2(volume.Width / 2.0f, volume.Height / 2.0f);

            var newVolume = new Rectangle(
                (int)(position.X - (int)origin.X),
                (int)(position.Y - (int)origin.Y),
                volume.Width,
                volume.Height);

            // update data
            GameState.Volumes[entityId] = newVolume;
        }
    }
}