using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Components
{
    internal class ProjectilePhysicsComponent : UpdateComponent
    {
        private Vector2 _position;
        private readonly Vector2 _velocity;
        private Rectangle _volume;
        private Vector2 Size => new Vector2(_volume.Width, _volume.Height);

        internal ProjectilePhysicsComponent(Vector2 position, Vector2 velocity)
        {
            _position = position;
            _velocity = velocity;
            _volume = new Rectangle(0, 0, 1, 8);
            DetermineBoundingBox();
        }

        public override void Update(GameTime gameTime)
        {
            // movement
            _position = _position + _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            DetermineBoundingBox();

            ResolveCollisions();

            Send();
        }

        private void DetermineBoundingBox()
        {
            Vector2 origin = Size / 2.0f;

            _volume = new Rectangle(
                (int)(_position.X - (int)origin.X),
                (int)(_position.Y - (int)origin.Y),
                (int)Size.X,
                (int)Size.Y);
        }

        private void ResolveCollisions()
        {
        }

        #region Send & Receive
        public void Send()
        {
            Communicator.Instance.Send(EntityId, ComponentType.VolumeGraphics, AttributeType.GraphicsVolume, _volume);
            Communicator.Instance.Send(EntityId, ComponentType.Graphics, AttributeType.GraphicsPosition, _position);
        }

        public override void Receive(AttributeType attributeId, object payload)
        {
        }
        #endregion
    }
}