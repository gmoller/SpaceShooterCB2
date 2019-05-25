using Microsoft.Xna.Framework;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    public interface IPhysicsComponent : IComponent
    {
        void Update(Player player, GameTime gameTime);
        PhysicsBody Body { get; }
    }

    internal class PlayerPhysicsComponent : IPhysicsComponent
    {
        private const float MOVE_SPEED = 240.0f; // pixels per second

        private Vector2 _velocity;
        public PhysicsBody Body { get; }

        internal PlayerPhysicsComponent(Player player)
        {
            Body = new PhysicsBody();
            SetupBoundingBox(player);
        }

        private void SetupBoundingBox(Player player)
        {
            var origin = new Vector2(8.0f * player.Scale.X, 8.0f * player.Scale.Y);

            Body.BoundingBox = new Rectangle(
                (int)(player.Position.X - (int)origin.X),
                (int)(player.Position.Y - (int)origin.Y),
                (int)(16.0f * player.Scale.X),
                (int)(16.0f * player.Scale.Y));
        }

        public void Update(Player player, GameTime gameTime)
        {
            player.Position = new Vector2(player.Position.X + _velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, player.Position.Y + _velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            SetupBoundingBox(player);

            // do not allow our player off the screen
            float x = Body.BoundingBox.Width / 2.0f;
            float y = Body.BoundingBox.Height / 2.0f;
            player.Position = new Vector2(
                MathHelper.Clamp(player.Position.X, x, DeviceManager.Instance.ScreenWidth - x),
                MathHelper.Clamp(player.Position.Y, y, DeviceManager.Instance.ScreenHeight - y));
        }

        public void Receive(object payload)
        {
            _velocity = (Vector2)payload * MOVE_SPEED;
        }
    }
}