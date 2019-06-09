using System.Diagnostics;
using GameEngineCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShooterLogic.Creators;
using SpaceShooterLogic.Screens;
using SpaceShooterLogic.Systems;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.GameStates
{
    public class GamePlayState : IGameState
    {
        private readonly Stopwatch _updateStopwatch = new Stopwatch();
        private readonly Stopwatch _drawStopwatch = new Stopwatch();
        private int _updateFrames;
        private int _drawFrames;

        private readonly Systems.Systems _systems;
        private readonly Hud _hud;

        private readonly SoundEffectPlayer _soundEffectPlayer;
        private readonly Renderer _renderer;

        private readonly GameState _gameState;

        public GamePlayState(GameState gameState)
        {
            _gameState = gameState;

            // setup components
            _gameState = new GameState();

            // setup systems
            var systems = new Systems.System[]
            {
                new PlayerInputSystem("PlayerInput", _gameState),
                new MovementSystem("Movement", _gameState),
                new ClampToViewportSystem("ClampToViewport", _gameState),
                new DestroyIfOutsideViewportSystem("DestroyIfOutsideViewport", _gameState),
                new SetBoundingBoxSystem("SetBoundingBox", _gameState),
                new PlayerFireProjectileSystem("FireProjectile", _gameState),
                new EnemyFireProjectileSystem("EnemyFireProjectile", _gameState),
                new EnemyChaseSystem("EnemyChase", _gameState), 
                new AnimationSystem("Animation", _gameState),
                new PlayerCollisionDetectionSystem("PlayerCollisionDetection", _gameState),
                new ProjectileCollisionDetectionSystem("ProjectileCollisionDetection", _gameState),
                new RestorePlayerSystem("KillPlayer", _gameState),
                new CollisionResolutionSystem("CollisionResolution", _gameState),
                new EnemySpawnSystem("EnemySpawn", _gameState),
                new IsGameOverSystem("IsGameOver", _gameState), 
                new RenderingSystem("Rendering", _gameState)
            };
            _systems = new Systems.Systems(_gameState, systems);

            _hud = new Hud(_gameState);

            _soundEffectPlayer = new SoundEffectPlayer();
            _renderer = new Renderer();

            PlayerCreator.Create(_gameState, 0, 3);
            SpawnCreator.Create(_gameState);
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

            _systems.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);

            _hud.Update(gameTime);

            if (_gameState.GameOver)
            {
                return (this, new GameOverState(_gameState));
            }

            if (KeyboardHandler.IsKeyPressed(Keys.Pause))
            {
                return (this, new PausedState(_gameState));
            }

            _updateStopwatch.Stop();
            _gameState.Metrics["GamePlayState.Update"] = new Metric(_updateStopwatch.Elapsed.TotalMilliseconds, _updateFrames);

            return (this, this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _drawFrames++;
            _drawStopwatch.Start();

            _soundEffectPlayer.Play(_gameState);
            _renderer.Render(spriteBatch, _gameState);

            _hud.Draw(spriteBatch);

            _drawStopwatch.Stop();
            _gameState.Metrics["GamePlayState.Draw"] = new Metric(_drawStopwatch.Elapsed.TotalMilliseconds, _drawFrames);
        }

        public IGameState Clone()
        {
            return this;
        }
    }
}