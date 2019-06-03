using System.Collections.Generic;
using System.Diagnostics;
using AnimationLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GuiControls;
using SpaceShooterLogic.GameStates;
using SpaceShooterUtilities;

namespace SpaceShooterLogic
{
    public class SpaceShooterGame : IGame
    {
        private FramesPerSecondCounter _fps;
        private SpriteFont _font;
        private ScrollingBackground _scrollingBackground;

        private IGameState _gameState;

        private Label _lblTest;
        private Label _lblFps;

        private readonly Stopwatch _updateStopwatch = new Stopwatch();
        private readonly Stopwatch _drawStopwatch = new Stopwatch();
        private int _updateFrames;
        private int _drawFrames;

        public void Initialize(GameWindow window, GraphicsDevice graphicsDevice)
        {
            ContentLoader.LoadContent(graphicsDevice);
        }

        public void LoadContent(ContentManager content, int width, int height)
        {
            AssetsManager.Instance.ContentManager = content;
            DeviceManager.Instance.Viewport = new Rectangle(0, 0, width, height);

            // Load textures
            AssetsManager.Instance.AddTextures("sprBg0", "sprBg1", "sprBtnPlay", "sprBtnPlayDown", "sprBtnPlayHover", "sprBtnRestart", "sprBtnRestartDown", "sprBtnRestartHover", "sprPlayer", "sprLaserPlayer", "sprLaserEnemy0", "sprExplosion", "sprEnemy0", "sprEnemy1", "sprEnemy2", "Explosion10", "Explosion50", "Fireball02");
            // Load sounds
            AssetsManager.Instance.AddSounds("sndBtnDown", "sndBtnOver", "sndLaser", "sndExplode0", "sndExplode1");
            // Load sprite fonts
            AssetsManager.Instance.AddSpriteFonts("arialHeading", "arialSmall", "arialTiny");
            _font = AssetsManager.Instance.GetSpriteFont("arialTiny");

            _scrollingBackground = new ScrollingBackground(new List<string> { "sprBg0", "sprBg1" });

            _gameState = new MainMenuState();
            _gameState.Enter();

            _fps = new FramesPerSecondCounter();

            _lblTest = new Label(_font, VerticalAlignment.Top, HorizontalAlignment.Left, new Vector2(0.0f, 0.0f), "Testing, testing, testing, 1, 2, 3...", Color.Cyan, 1.0f, 0.5f);
            _lblFps = new Label(_font, VerticalAlignment.Bottom, HorizontalAlignment.Right, DeviceManager.Instance.ScreenDimensions, "FPS: ", Color.Cyan) { TextShadow = true };

            AssetsManager.Instance.AddAnimation("sprPlayer", AnimationSpecCreator.Create("sprPlayer", 64, 16, 16, 16, 160, true));
            AssetsManager.Instance.AddAnimation("sprEnemy0", AnimationSpecCreator.Create("sprEnemy0", 64, 16, 16, 16, 160, true));
            AssetsManager.Instance.AddAnimation("sprEnemy1", AnimationSpecCreator.Create("sprEnemy1", 16, 16, 16, 16, 160, true));
            AssetsManager.Instance.AddAnimation("sprEnemy2", AnimationSpecCreator.Create("sprEnemy2", 64, 16, 16, 16, 160, true));
            AssetsManager.Instance.AddAnimation("Fireball02", AnimationSpecCreator.Create("Fireball02", 512, 512, 128, 128, 20, false));
            AssetsManager.Instance.AddAnimation("Explosion10", AnimationSpecCreator.Create("Explosion10", 2048, 1536, 256, 256, 20, false));
        }

        public void Update(GameTime gameTime)
        {
            _updateFrames++;
            _updateStopwatch.Start();

            _fps.Update(gameTime);
            _scrollingBackground.Update(gameTime);

            (IGameState currentGameState, IGameState newGameState) returnGameState = _gameState.Update(gameTime);
            ChangeGameState(returnGameState.currentGameState, returnGameState.newGameState);

            _updateStopwatch.Stop();
            //BenchmarkMetrics.Instance.Metrics["SpaceShooterGame.Update"] = new Metric(_updateStopwatch.Elapsed.TotalMilliseconds, _updateFrames);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _drawFrames++;
            _drawStopwatch.Start();

            _fps.Draw();
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);

            _scrollingBackground.Draw(spriteBatch);
            _gameState.Draw(spriteBatch);

            _lblTest.Draw(spriteBatch);
            _lblFps.Text = $"FPS: {_fps.FramesPerSecond}";
            _lblFps.Draw(spriteBatch);

            spriteBatch.End();

            _drawStopwatch.Stop();
            //BenchmarkMetrics.Instance.Metrics["SpaceShooterGame.Draw"] = new Metric(_drawStopwatch.Elapsed.TotalMilliseconds, _drawFrames);
        }

        private void ChangeGameState(IGameState currentGameState, IGameState newGameState)
        {
            if (currentGameState == newGameState) return;

            if (currentGameState is MainMenuState && newGameState is GamePlayState ||
                currentGameState is GamePlayState && newGameState is GameOverState ||
                currentGameState is GameOverState && newGameState is GamePlayState)
            {
                _gameState.Leave();
                _gameState = newGameState;
                _gameState.Enter();
            }

            if (currentGameState is GamePlayState && newGameState is PausedState ||
                currentGameState is PausedState && newGameState is GamePlayState)
            {
                _gameState = newGameState;
            }
        }
    }
}