using System;
using GameEngineCore;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Components
{
    internal class ChasingBehaviorComponent : UpdateComponent
    {
        private const float RANGE_TO_START_CHASING = 320.0f;
        private const float CHASING_MOVE_SPEED = 0.2f; // pixels per millisecond
        private const float ROTATION_SPEED = 0.005f;

        private float _angle;
        private ChasingState _state = ChasingState.MoveDown;

        public Vector2 Position { get; private set; }

        internal ChasingBehaviorComponent()
        {
            _angle = 0.0f;
        }

        public override void Update(float deltaTime)
        {
            // find player
            Entities entities = Registrar.Instance.FilterEntities(Operator.And, typeof(PlayerPhysicsComponent));
            if (entities.Count == 1)
            {
                ComponentsSet entity = entities[0];
                var physicsComponent = entity[typeof(PlayerPhysicsComponent)] as PlayerPhysicsComponent;

                if (Vector2.Distance(Position, physicsComponent.Position) < RANGE_TO_START_CHASING)
                {
                    _state = ChasingState.Chase;
                }

                if (_state == ChasingState.Chase)
                {
                    Vector2 direction = physicsComponent.Position - Position;
                    direction.Normalize();
                    Vector2 velocity = direction * CHASING_MOVE_SPEED;
                    float rotationSpeed = ROTATION_SPEED * deltaTime;
                    if (Position.X < physicsComponent.Position.X) // is ChaserShip to the left or right of player?
                    {
                        _angle -= rotationSpeed;
                    }
                    else
                    {
                        _angle += rotationSpeed;
                    }

                    Send(velocity, _angle);
                }
            }
        }

        #region Send & Receive
        private void Send(Vector2 velocity, float angle)
        {
            Communicator.Instance.Send(EntityId, typeof(EnemyPhysicsComponent), nameof(EnemyPhysicsComponent.Velocity), velocity);
            Communicator.Instance.Send(EntityId, typeof(GraphicsComponent), nameof(GraphicsComponent.Rotation), angle);
        }

        public override void Receive(string attributeName, object payload)
        {
            switch (attributeName)
            {
                case "Position":
                    Position = (Vector2)payload;
                    break;
                default:
                    throw new NotSupportedException($"Attribute [{attributeName}] is not supported by ChasingBehaviorComponent.");
            }
        }
        #endregion

        private enum ChasingState
        {
            MoveDown,
            Chase
        }
    }
}