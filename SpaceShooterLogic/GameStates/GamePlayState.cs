using System.Diagnostics;
using GameEngineCore;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterLogic.Components;
using SpaceShooterLogic.Creators;
using SpaceShooterLogic.Screens;
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
            InputComponent = new PlayerInputComponent(); // TODO: investigate this 1
        }

        public (bool changeGameState, IGameState newGameState) Update(GameTime gameTime)
        {
            _updateFrames++;
            _updateStopwatch.Start();

            //Entities entities = Registrar.Instance.GetAllEntities();
            Entities entities = Registrar.Instance.FilterEntities(typeof(PlayerPhysicsComponent)); // oops, what about explosion - no physics
            foreach (ComponentsSet componentsSet in entities)
            {
                var entity = new Entity2(componentsSet);
                entity.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            entities = Registrar.Instance.FilterEntities(typeof(ProjectilePhysicsComponent)); // oops, what about explosion - no physics
            foreach (ComponentsSet componentsSet in entities)
            {
                var entity = new Entity2(componentsSet);
                entity.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            //GameEntitiesManager.Instance.Enemies.Update(gameTime);
            //GameEntitiesManager.Instance.EnemyProjectiles.Update(gameTime);
            GameEntitiesManager.Instance.Hud.Update(gameTime);

            if (GameEntitiesManager.Instance.PlayerIsDead)
            {
                bool changeToGameOverState = CheckForChangeToGameOverState(gameTime);
                if (changeToGameOverState)
                {
                    return (true, new GameOverState());
                }
            }

            _updateStopwatch.Stop();
            BenchmarkMetrics.Instance.Metrics["GamePlayState.Update"] = new Metric(_updateStopwatch.Elapsed.TotalMilliseconds, _updateFrames);

            return (false, null);
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

            Entities entities = Registrar.Instance.FilterEntities(typeof(GraphicsComponent));
            foreach (ComponentsSet componentsSet in entities)
            {
                var entity = new Entity2(componentsSet);
                entity.Draw(spriteBatch);
            }

            //GameEntitiesManager.Instance.Enemies.Draw(spriteBatch);
            //GameEntitiesManager.Instance.EnemyProjectiles.Draw(spriteBatch);
            GameEntitiesManager.Instance.Hud.Draw(spriteBatch);

            _drawStopwatch.Stop();
            BenchmarkMetrics.Instance.Metrics["GamePlayState.Draw"] = new Metric(_drawStopwatch.Elapsed.TotalMilliseconds, _drawFrames);
        }

        private void ResetLevel()
        {
            CreatePlayer();
            CreateEnemy();
            //CreateEnemySpawner();
            //GameEntitiesManager.Instance.Enemies = new Enemies.Enemies();
            GameEntitiesManager.Instance.EnemyProjectiles = new Projectiles();
            GameEntitiesManager.Instance.Hud = new Hud();
        }

        private void CreatePlayer()
        {
            PlayerCreator.Create(InputComponent);
            GameEntitiesManager.Instance.PlayerIsDead = false;
        }

        private void CreateEnemy()
        {
            EnemyCreator.Create(new Vector2(50.0f, 16.0f), new Vector2(0.0f, 0.005f)); // pixels per millisecond
        }
    }
}