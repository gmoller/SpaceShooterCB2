using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.GameStates
{
    public class PausedState : IGameState
    {
        private readonly Stopwatch _drawStopwatch = new Stopwatch();
        private int _drawFrames;

        private IGameState _previousGameState;

        private readonly GameState _gameState;

        public PausedState(GameState gameState)
        {
            _gameState = gameState;
        }

        public void Enter(IGameState previousGameState)
        {
            _previousGameState = previousGameState;
        }

        public void Leave()
        {
        }

        public (IGameState currentGameState, IGameState newGameState) Update(GameTime gameTime)
        {
            if (KeyboardHandler.IsKeyPressed(Keys.Pause))
            {
                return (this, _previousGameState);
            }

            return (this, this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _drawFrames++;
            _drawStopwatch.Start();

            // TODO: draw rendering system

            _gameState.Hud.Draw(spriteBatch);

            _drawStopwatch.Stop();

            //_gameState.Metrics["GamePlayState.Draw"] = new Metric(_drawStopwatch.Elapsed.TotalMilliseconds, _drawFrames);
        }

        public IGameState Clone()
        {
            return this;
        }
    }
}