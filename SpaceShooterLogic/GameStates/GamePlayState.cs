using System.Diagnostics;
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

            // TODO: Explosion is currently a surrogate for a generic entity. Rename this to entity!!!!!
            Entities entities = Registrar.Instance.FilterEntities(ComponentType.Physics);
            //Entities entities = Registrar.Instance.GetAllEntities();
            foreach (ComponentsSet componentsSet in entities)
            {
                var entity = new Explosion(componentsSet);
                entity.Update(gameTime);
            }

            //GameEntitiesManager.Instance.PlayerProjectiles.Update(gameTime);
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

            Entities entities = Registrar.Instance.FilterEntities(ComponentType.Graphics);
            foreach (ComponentsSet componentsSet in entities)
            {
                var entity = new Explosion(componentsSet);
                entity.Draw(spriteBatch);
            }

            //GameEntitiesManager.Instance.PlayerProjectiles.Draw(spriteBatch);
            //GameEntitiesManager.Instance.Enemies.Draw(spriteBatch);
            //GameEntitiesManager.Instance.EnemyProjectiles.Draw(spriteBatch);
            GameEntitiesManager.Instance.Hud.Draw(spriteBatch);

            _drawStopwatch.Stop();
            BenchmarkMetrics.Instance.Metrics["GamePlayState.Draw"] = new Metric(_drawStopwatch.Elapsed.TotalMilliseconds, _drawFrames);
        }

        private void ResetLevel()
        {
            CreatePlayer();
            //GameEntitiesManager.Instance.PlayerProjectiles = new Projectiles();
            GameEntitiesManager.Instance.Enemies = new Enemies.Enemies();
            GameEntitiesManager.Instance.EnemyProjectiles = new Projectiles();
            GameEntitiesManager.Instance.Hud = new Hud();
        }

        private void CreatePlayer()
        {
            PlayerCreator.Create(InputComponent);
            GameEntitiesManager.Instance.PlayerIsDead = false;
        }
    }
}