using GameEngineCore;
using Microsoft.Xna.Framework;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class MovementSystem : System
    {
        public MovementSystem(string name) : base(name)
        {
        }

        protected override void ProcessOne(int entityId, float deltaTime)
        {
            #region gather data
            var position = Registrar.Instance.GetComponent<Vector2?>("Positions", entityId);
            var velocity = Registrar.Instance.GetComponent<Vector2?>("Velocities", entityId);
            var volume = Registrar.Instance.GetComponent<Rectangle?>("Volumes", entityId);
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
                Registrar.Instance.SetComponent<Vector2?>("Positions", entityId, newPosition);
                Registrar.Instance.SetComponent<Rectangle?>("Volumes", entityId, newVolume);
                #endregion
            }
            #endregion
        }
    }
}