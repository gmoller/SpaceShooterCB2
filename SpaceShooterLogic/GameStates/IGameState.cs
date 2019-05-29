using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooterLogic.GameStates
{
    public interface IGameState
    {
        void Enter();
        void Leave();
        (IGameState currentGameState, IGameState newGameState) Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}