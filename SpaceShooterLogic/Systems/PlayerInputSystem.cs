using GameEngineCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class PlayerInputSystem : System
    {
        private const float MOVE_SPEED = 0.24f; // pixels per millisecond

        public PlayerInputSystem(string name) : base(name)
        {
        }

        protected override void ProcessOne(int entityId, float deltaTime)
        {
            #region gather data
            var position = Registrar.Instance.GetComponent<object>("PlayerInputTags", entityId);
            #endregion

            #region process data
            if (position != null)
            {
                var direction = Vector2.Zero;
                var shoot = false;
                if (KeyboardHandler.IsKeyDown(Keys.Up))
                {
                    direction = new Vector2(direction.X, -1.0f);
                }
                if (KeyboardHandler.IsKeyDown(Keys.Down))
                {
                    direction = new Vector2(direction.X, 1.0f);
                }
                if (KeyboardHandler.IsKeyDown(Keys.Left))
                {
                    direction = new Vector2(-1.0f, direction.Y);
                }
                if (KeyboardHandler.IsKeyDown(Keys.Right))
                {
                    direction = new Vector2(1.0f, direction.Y);
                }
                if (KeyboardHandler.IsKeyDown(Keys.RightControl))
                {
                    shoot = true;
                }

                #region update data
                Registrar.Instance.SetComponent<Vector2?>("Velocities", entityId, direction * MOVE_SPEED);
                #endregion
            }
            #endregion
        }
    }
}