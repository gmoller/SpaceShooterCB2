using System.Collections.Generic;
using System.Diagnostics;
using GameEngineCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShooterLogic.Creators;
using SpaceShooterLogic.Systems;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.GameStates
{
    public class GamePlayState : IGameState
    {
        private const int NUMBER_OF_THREADS = 16;

        private float _timeElapsedSinceDied; // in seconds
        private readonly int _restartDelay = 3; // in seconds

        private readonly Stopwatch _updateStopwatch = new Stopwatch();
        private readonly Stopwatch _drawStopwatch = new Stopwatch();
        private int _updateFrames;
        private int _drawFrames;

        private readonly List<Systems.System> _systems;
        private readonly SoundEffectPlayer _soundEffectPlayer;
        private readonly Renderer _renderer;

        private readonly GameState _gameState;

        public GamePlayState(GameState gameState)
        {
            _gameState = gameState;

            // setup components
            _gameState = new GameState();

            // setup systems
            _systems = new List<Systems.System>
            {
                new PlayerInputSystem("PlayerInput", _gameState),
                new MovementSystem("Movement", _gameState),
                new ClampToViewportSystem("ClampToViewport", _gameState),
                new DestroyIfOutsideViewportSystem("DestroyIfOutsideViewport", _gameState),
                new SetBoundingBoxSystem("SetBoundingBox", _gameState),
                new FireProjectileSystem("FireProjectile", _gameState),
                new EnemyFireProjectileSystem("EnemyFireProjectile", _gameState),
                new AnimationSystem("Animation", _gameState),
                new PlayerCollisionDetectionSystem("PlayerCollisionDetection", _gameState),
                new ProjectileCollisionDetectionSystem("ProjectileCollisionDetection", _gameState),
                new KillPlayerSystem("KillPlayer", _gameState),
                new CollisionResolutionSystem("CollisionResolution", _gameState),
                new EnemySpawnSystem("EnemySpawn", _gameState),
                new RenderingSystem("Rendering", _gameState)
            };

            _soundEffectPlayer = new SoundEffectPlayer();
            _renderer = new Renderer();

            PlayerCreator.Create(_gameState);
            SpawnCreator.Create(_gameState);
            //EnemyCreator.Create(new Vector2(50.0f, 16.0f), new Vector2(0.0f, 0.005f)); // pixels per millisecond
        }

        public virtual void Enter(IGameState previousGameState)
        {
        }

        public virtual void Leave()
        {
        }

        public (IGameState currentGameState, IGameState newGameState) Update(GameTime gameTime)
        {
            _updateFrames++;
            _updateStopwatch.Start();

            foreach (var system in _systems)
            {
                system.Process((float)gameTime.ElapsedGameTime.TotalMilliseconds, NUMBER_OF_THREADS);
            }

            _gameState.Hud.Update(gameTime);

            if (_gameState.PlayerIsDead)
            {
                bool changeToGameOverState = CheckForChangeToGameOverState(gameTime);
                if (changeToGameOverState)
                {
                    return (this, new GameOverState(_gameState));
                }
            }

            if (KeyboardHandler.IsKeyPressed(Keys.Pause))
            {
                return (this, new PausedState(_gameState));
            }

            _updateStopwatch.Stop();
            _gameState.Metrics["GamePlayState.Update"] = new Metric(_updateStopwatch.Elapsed.TotalMilliseconds, _updateFrames);

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
            if (_gameState.Lives > 1)
            {
                ResetLevel();
                return false;
            }

            // out of lives, game over!
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _drawFrames++;
            _drawStopwatch.Start();

            _soundEffectPlayer.Play(_gameState);
            _renderer.Render(spriteBatch, _gameState);

            _gameState.Hud.Draw(spriteBatch);

            _drawStopwatch.Stop();
            _gameState.Metrics["GamePlayState.Draw"] = new Metric(_drawStopwatch.Elapsed.TotalMilliseconds, _drawFrames);
        }

        private void ResetLevel()
        {
            _gameState.Restart();

            PlayerCreator.Create(_gameState);
            SpawnCreator.Create(_gameState);
            //EnemyCreator.Create(new Vector2(50.0f, 16.0f), new Vector2(0.0f, 0.005f)); // pixels per millisecond
        }

        public IGameState Clone()
        {
            return this;
        }
    }
}