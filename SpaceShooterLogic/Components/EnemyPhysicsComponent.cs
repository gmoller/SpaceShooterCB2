using System;
using GameEngineCore;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Components
{
    internal class EnemyPhysicsComponent : UpdateComponent
    {
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        public Rectangle Volume { get; private set; }
        public Vector2 Size => new Vector2(Volume.Width, Volume.Height);
        public int Score { get; }

        internal EnemyPhysicsComponent(Vector2 position, Vector2 velocity, Vector2 size, int score)
        {
            Position = position;
            Velocity = velocity;
            Volume = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            DetermineBoundingBox();
            Score = score;
        }

        public override void Update(float deltaTime)
        {
            // movement
            Position = Position + Velocity * deltaTime;
            DetermineBoundingBox();

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

        #region Send & Receive
        private void Send()
        {
            Communicator.Instance.Send(EntityId, typeof(ChasingBehaviorComponent), nameof(ChasingBehaviorComponent.Position), Position);
            Communicator.Instance.Send(EntityId, typeof(VolumeGraphicsComponent), nameof(VolumeGraphicsComponent.Volume), Volume);
            Communicator.Instance.Send(EntityId, typeof(GraphicsComponent), nameof(GraphicsComponent.Position), Position);
        }

        public override void Receive(string attributeName, object payload)
        {
            switch (attributeName)
            {
                case "Velocity":
                    Velocity = (Vector2)payload;
                    break;
                default:
                    throw new NotSupportedException($"Attribute [{attributeName}] is not supported by EnemyPhysicsComponent.");
            }
        }
        #endregion
    }
}