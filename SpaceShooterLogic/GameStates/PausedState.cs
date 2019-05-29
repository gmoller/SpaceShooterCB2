using System.Diagnostics;
using GameEngineCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.GameStates
{
    public class PausedState : IGameState
    {
        private readonly Stopwatch _drawStopwatch = new Stopwatch();
        private int _drawFrames;

        public void Enter()
        {
        }

        public void Leave()
        {
        }

        public (bool changeGameState, IGameState newGameState) Update(GameTime gameTime)
        {
            if (KeyboardHandler.IsKeyPressed(Keys.Pause))
            {
                return (true, new GamePlayState());
            }

            return (false, null);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _drawFrames++;
            _drawStopwatch.Start();

            Entities entities = Registrar.Instance.FilterEntities(Operator.And, typeof(GraphicsComponent));
            foreach (ComponentsSet componentsSet in entities)
            {
                var entity = new Entity(componentsSet);
                entity.Draw(spriteBatch);
            }

            GameEntitiesManager.Instance.Hud.Draw(spriteBatch);

            _drawStopwatch.Stop();
            BenchmarkMetrics.Instance.Metrics["GamePlayState.Draw"] = new Metric(_drawStopwatch.Elapsed.TotalMilliseconds, _drawFrames);
        }
    }
}