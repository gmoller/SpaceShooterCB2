using System.Collections.Generic;
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
        private const int NUMBER_OF_THREADS = 16;
        private const int NUMBER_OF_DRAW_THREADS = 8;

        protected UpdateComponent InputComponent;

        private float _timeElapsedSinceDied; // in seconds
        private readonly int _restartDelay = 3; // in seconds

        private readonly Stopwatch _updateStopwatch = new Stopwatch();
        private readonly Stopwatch _drawStopwatch = new Stopwatch();
        private int _updateFrames;
        private int _drawFrames;

        private List<Systems.System> _systems;
        private RenderingSystem _renderingSystem;

        public virtual void Enter(IGameState previousGameState)
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

            foreach (var system in _systems)
            {
                system.Process((float)gameTime.ElapsedGameTime.TotalMilliseconds, NUMBER_OF_THREADS);
            }

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
            _renderingSystem.Process(0.0f, NUMBER_OF_DRAW_THREADS);

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
            // setup components
            var state = new GameState();

            // setup systems
            _systems = new List<Systems.System>
            {
                new PlayerInputSystem("PlayerInput", state),
                new MovementSystem("Movement", state),
                new ClampToViewportSystem("ClampToViewport", state),
                new DestroyIfOutsideViewportSystem("DestroyIfOutsideViewport", state),
                new SetBoundingBoxSystem("SetBoundingBox", state),
                new FireProjectileSystem("FireProjectile", state),
                new AnimationSystem("Animation", state)
            };

            _renderingSystem = new RenderingSystem("Rendering", state);

            Registrar.Instance.Clear();

            SetController();
            PlayerCreator.Create(InputComponent, state);
            SpawnCreator.Create();
            //EnemyCreator.Create(new Vector2(50.0f, 16.0f), new Vector2(0.0f, 0.005f)); // pixels per millisecond

            GameEntitiesManager.Instance.Hud = new Hud();
            GameEntitiesManager.Instance.PlayerIsDead = false;
        }

        public IGameState Clone()
        {
            return this;
        }
    }
}