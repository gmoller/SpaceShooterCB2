using GameEngineCore;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Components
{
    internal class ProjectilePhysicsComponent : UpdateComponent
    {
        private readonly Vector2 _velocity;

        private Vector2 _position;
        private Vector2 Size => new Vector2(Volume.Width, Volume.Height);

        public Rectangle Volume { get; private set; }

        internal ProjectilePhysicsComponent(Vector2 position, Vector2 velocity, Vector2 size)
        {
            _position = position;
            _velocity = velocity;
            Volume = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            DetermineBoundingBox();
        }

        public override void Update(float deltaTime)
        {
            // movement
            _position = _position + _velocity * deltaTime;
            DetermineBoundingBox();

            ResolveCollisions();

            Send();
        }

        private void DetermineBoundingBox()
        {
            Vector2 origin = Size / 2.0f;

            Volume = new Rectangle(
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
            Communicator.Instance.Send(EntityId, typeof(VolumeGraphicsComponent), nameof(VolumeGraphicsComponent.Volume), Volume);
            Communicator.Instance.Send(EntityId, typeof(GraphicsComponent), nameof(GraphicsComponent.Position), _position);
        }

        public override void Receive(string attributeName, object payload)
        {
        }
        #endregion
    }
}