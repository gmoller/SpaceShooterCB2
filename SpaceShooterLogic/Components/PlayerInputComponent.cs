using System;
using System.Collections.Generic;
using GameEngineCore;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Components
{
    internal class PlayerInputComponent : UpdateComponent
    {
        private readonly IPlayerController _playerController;
        private Vector2 _playerPosition;
        private Vector2 _velocity;
        private bool _shootLaser;

        internal PlayerInputComponent()
        {
            _playerController = new PlayerController();
        }

        public override void Update(float deltaTime)
        {
            List<PlayerAction> playerActions = _playerController.GetActions();

            _velocity = Vector2.Zero;
            _shootLaser = false;

            foreach (PlayerAction playerAction in playerActions)
            {
                switch (playerAction)
                {
                    case PlayerAction.None:
                        // do nothing
                        break;
                    case PlayerAction.MoveUp:
                        _velocity = new Vector2(_velocity.X, -1.0f);
                        break;
                    case PlayerAction.MoveDown:
                        _velocity = new Vector2(_velocity.X, 1.0f);
                        break;
                    case PlayerAction.MoveLeft:
                        _velocity = new Vector2(-1.0f, _velocity.Y);
                        break;
                    case PlayerAction.MoveRight:
                        _velocity = new Vector2(1.0f, _velocity.Y);
                        break;
                    case PlayerAction.FireLaser:
                        _shootLaser = true;
                        break;
                }
            }

            Send();
        }

        #region Send & Receive
        public void Send()
        {
            Communicator.Instance.Send(EntityId, ComponentType.Physics, AttributeType.PhysicsVelocity, _velocity);
            if (_shootLaser) Communicator.Instance.Send(EntityId, ComponentType.Laser, AttributeType.LaserShootLaser, _playerPosition);
        }

        public override void Receive(AttributeType attributeId, object payload)
        {
            switch (attributeId)
            {
                case AttributeType.InputPlayerPosition:
                    _playerPosition = (Vector2)payload;
                    break;
                default:
                    throw new NotSupportedException($"Attribute Id [{attributeId}] is not supported by PlayerInputComponent.");
            }
        }
        #endregion
    }
}