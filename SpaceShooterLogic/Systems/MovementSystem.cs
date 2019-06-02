using Microsoft.Xna.Framework;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class MovementSystem : System
    {
        // Components: Position, Velocity, Volume

        public MovementSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOne(int entityId, float deltaTime)
        {
            #region gather data
            var position = GameState.Positions[entityId];
            var velocity = GameState.Velocities[entityId];
            var volume = GameState.Volumes[entityId];
            #endregion

            #region process data
            if (position != null && velocity != null && volume != null)
            {
                // calculate new position
                var newPosition = position.Value + velocity.Value * deltaTime;

                // calculate new Bounding Box
                var vol = volume.Value;
                var origin = new Vector2(vol.Width / 2.0f, vol.Height / 2.0f);

                var newVolume = new Rectangle(
                    (int)(newPosition.X - (int)origin.X),
                    (int)(newPosition.Y - (int)origin.Y),
                    vol.Width,
                    vol.Height);

                // do not allow our player off the screen
                var x = vol.Width / 2.0f;
                var y = vol.Height / 2.0f;
                newPosition = new Vector2(
                    MathHelper.Clamp(newPosition.X, x, DeviceManager.Instance.ScreenWidth - x),
                    MathHelper.Clamp(newPosition.Y, y, DeviceManager.Instance.ScreenHeight - y));

                #region update data
                GameState.Positions[entityId] = newPosition;
                GameState.Volumes[entityId] = newVolume;
                #endregion
            }
            #endregion
        }
    }
}