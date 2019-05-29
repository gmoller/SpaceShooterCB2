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

        public Vector2 PlayerPosition { get; private set; }

        internal PlayerInputComponent()
        {
            _playerController = new PlayerController();
        }

        public override void Update(float deltaTime)
        {
            List<PlayerAction> playerActions = _playerController.GetActions();

            Vector2 direction = Vector2.Zero;
            bool shootLaser = false;

            foreach (PlayerAction playerAction in playerActions)
            {
                switch (playerAction)
                {
                    case PlayerAction.None:
                        // do nothing
                        break;
                    case PlayerAction.MoveUp:
                        direction = new Vector2(direction.X, -1.0f);
                        break;
                    case PlayerAction.MoveDown:
                        direction = new Vector2(direction.X, 1.0f);
                        break;
                    case PlayerAction.MoveLeft:
                        direction = new Vector2(-1.0f, direction.Y);
                        break;
                    case PlayerAction.MoveRight:
                        direction = new Vector2(1.0f, direction.Y);
                        break;
                    case PlayerAction.FireLaser:
                        shootLaser = true;
                        break;
                }
            }

            Send(direction, shootLaser);
        }

        #region Send & Receive
        private void Send(Vector2 direction, bool shootLaser)
        {
            Communicator.Instance.Send(EntityId, typeof(PlayerPhysicsComponent), nameof(PlayerPhysicsComponent.Velocity), direction);
            if (shootLaser)
            {
                Communicator.Instance.Send(EntityId, typeof(LaserComponent), nameof(LaserComponent.ShootLaser), true);
                Communicator.Instance.Send(EntityId, typeof(LaserComponent), nameof(LaserComponent.FiringEntityPosition), PlayerPosition);
            }
        }

        public override void Receive(string attributeName, object payload)
        {
            switch (attributeName)
            {
                case "PlayerPosition":
                    PlayerPosition = (Vector2)payload;
                    break;
                default:
                    throw new NotSupportedException($"Attribute [{attributeName}] is not supported by PlayerInputComponent.");
            }
        }
        #endregion
    }
}