using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Components
{
    public interface IInputComponent : IComponent
    {
        void Update(Player player, GameTime gameTime);
    }

    internal class PlayerInputComponent : IInputComponent
    {
        private readonly IPlayerController _playerController;
        private Vector2 _playerPosition;

        internal PlayerInputComponent()
        {
            _playerController = new PlayerController();
        }

        public void Update(Player player, GameTime gameTime)
        {
            List<PlayerAction> playerActions = _playerController.GetActions();

            var velocity = Vector2.Zero;
            bool shootLaser = false;

            foreach (PlayerAction playerAction in playerActions)
            {
                switch (playerAction)
                {
                    case PlayerAction.None:
                        // do nothing
                        break;
                    case PlayerAction.MoveUp:
                        velocity = new Vector2(velocity.X, -1.0f);
                        break;
                    case PlayerAction.MoveDown:
                        velocity = new Vector2(velocity.X, 1.0f);
                        break;
                    case PlayerAction.MoveLeft:
                        velocity = new Vector2(-1.0f, velocity.Y);
                        break;
                    case PlayerAction.MoveRight:
                        velocity = new Vector2(1.0f, velocity.Y);
                        break;
                    case PlayerAction.FireLaser:
                        shootLaser = true;
                        break;
                }
            }

            //string msg1 = "Physics -> {velocity}";
            player.Send(ComponentType.Physics, velocity);

            if (shootLaser)
            {
                //string msg2 = "Laser -> {player.Position}";
                player.Send(ComponentType.Laser, _playerPosition);
            }
        }

        public void Receive(object payload)
        {
            _playerPosition = (Vector2)payload;
        }
    }
}