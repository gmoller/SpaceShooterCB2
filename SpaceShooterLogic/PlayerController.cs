using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using SpaceShooterUtilities;

namespace SpaceShooterLogic
{
    public enum PlayerAction
    {
        None,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        FireLaser
    }

    public interface IPlayerController
    {
        List<PlayerAction> GetActions();
    }

    public class PlayerController : IPlayerController
    {
        public List<PlayerAction> GetActions()
        {
            List<PlayerAction> playerActions = new List<PlayerAction>();

            if (KeyboardHandler.IsKeyDown(Keys.W))
            {
                playerActions.Add(PlayerAction.MoveUp);
            }

            if (KeyboardHandler.IsKeyDown(Keys.S))
            {
                playerActions.Add(PlayerAction.MoveDown);
            }

            if (KeyboardHandler.IsKeyDown(Keys.A))
            {
                playerActions.Add(PlayerAction.MoveLeft);
            }

            if (KeyboardHandler.IsKeyDown(Keys.D))
            {
                playerActions.Add(PlayerAction.MoveRight);
            }

            if (KeyboardHandler.IsKeyDown(Keys.Space))
            {
                playerActions.Add(PlayerAction.FireLaser);
            }

            return playerActions;
        }
    }
}