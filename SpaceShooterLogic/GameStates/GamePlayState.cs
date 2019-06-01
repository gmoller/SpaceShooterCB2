using System.Diagnostics;
using GameEngineCore;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShooterLogic.Components;
using SpaceShooterLogic.Creators;
using SpaceShooterLogic.Screens;
using SpaceShooterLogic.Systems;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.GameStates
{
    public class GamePlayState : IGameState
    {
        protected UpdateComponent InputComponent;

        private float _timeElapsedSinceDied; // in seconds
        private readonly int _restartDelay = 3; // in seconds

        private readonly Stopwatch _updateStopwatch = new Stopwatch();
        private readonly Stopwatch _drawStopwatch = new Stopwatch();
        private int _updateFrames;
        private int _drawFrames;

        private readonly PlayerInputSystem _playerInputSystem = new PlayerInputSystem("PlayerInput");
        private readonly MovementSystem _movementSystem = new MovementSystem("Movement");
        private readonly RenderingSystem _renderingSystem = new RenderingSystem("Rendering");

        public virtual void Enter()
        {
            SetController();
            ResetLevel();
            GameEntitiesManager.Instance.Score = 0;
            GameEntitiesManager.Instance.Lives = 3;
        }

        public virtual void Leave()
        {
            Registrar.Instance.Clear();
        }

        protected virtual void SetController()
        {
            InputComponent = new PlayerInputComponent();
        }

        public (IGameState currentGameState, IGameState newGameState) Update(GameTime gameTime)
        {
            _updateFrames++;
            _updateStopwatch.Start();

            _playerInputSystem.Process((float)gameTime.ElapsedGameTime.TotalMilliseconds, 16);
            _movementSystem.Process((float)gameTime.ElapsedGameTime.TotalMilliseconds, 16);

            //Entities entities = Registrar.Instance.GetAllEntities();
            Entities entities = Registrar.Instance.FilterEntities(Operator.Or, 
                typeof(PlayerPhysicsComponent), 
                typeof(EnemyPhysicsComponent), 
                typeof(ProjectilePhysicsComponent),
                typeof(SpriteComponent),
                typeof(SpawnComponent));
            foreach (ComponentsSet componentsSet in entities)
            {
                var entity = new Entity(componentsSet);
                entity.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            GameEntitiesManager.Instance.Hud.Update(gameTime);

            if (GameEntitiesManager.Instance.PlayerIsDead)
            {
                bool changeToGameOverState = CheckForChangeToGameOverState(gameTime);
                if (changeToGameOverState)
                {
                    return (this, new GameOverState());
                }
            }

            if (KeyboardHandler.IsKeyPressed(Keys.Pause))
            {
                return (this, new PausedState());
            }

            _updateStopwatch.Stop();
            BenchmarkMetrics.Instance.Metrics["GamePlayState.Update"] = new Metric(_updateStopwatch.Elapsed.TotalMilliseconds, _updateFrames);

            return (this, this);
        }

        private bool CheckForChangeToGameOverState(GameTime gameTime)
        {
            if (_timeElapsedSinceDied < _restartDelay)
            {
                _timeElapsedSinceDied += (float)gameTime.ElapsedGameTime.TotalSeconds;

                return false;
            }

            _timeElapsedSinceDied = 0.0f;
            if (GameEntitiesManager.Instance.Lives > 1)
            {
                ResetLevel();
                GameEntitiesManager.Instance.Lives--;
                SetController();
                return false;
            }

            // out of lives, game over!
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _drawFrames++;
            _drawStopwatch.Start();

            _renderingSystem.SpriteBatch = spriteBatch;
            _renderingSystem.Process(0.0f, 1);

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

        private void ResetLevel()
        {
            Registrar.Instance.Clear();

            SetController();
            PlayerCreator.Create(InputComponent);
            SpawnCreator.Create();
            //EnemyCreator.Create(new Vector2(50.0f, 16.0f), new Vector2(0.0f, 0.005f)); // pixels per millisecond

            GameEntitiesManager.Instance.Hud = new Hud();
            GameEntitiesManager.Instance.PlayerIsDead = false;
        }
    }
}