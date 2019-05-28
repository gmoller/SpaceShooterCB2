using GameEngineCore;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Components
{
    internal class PhysicsComponent : UpdateComponent
    {
        private readonly Vector2 _velocity;

        public Vector2 Position { get; private set; }
        public Rectangle Volume { get; private set; }
        public Vector2 Size => new Vector2(Volume.Width, Volume.Height);

        internal PhysicsComponent(Vector2 position, Vector2 velocity, Vector2 size)
        {
            Position = position;
            _velocity = velocity;
            Volume = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            DetermineBoundingBox();
        }

        public override void Update(float deltaTime)
        {
            // movement
            Position = Position + _velocity * deltaTime;
            DetermineBoundingBox();

            ResolveCollisions();

            Send();
        }

        private void DetermineBoundingBox()
        {
            Vector2 origin = Size / 2.0f;

            Volume = new Rectangle(
                (int)(Position.X - (int)origin.X),
                (int)(Position.Y - (int)origin.Y),
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
            Communicator.Instance.Send(EntityId, typeof(GraphicsComponent), nameof(GraphicsComponent.Position), Position);
        }

        public override void Receive(string attributeName, object payload)
        {
        }
        #endregion
    }
}