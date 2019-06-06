using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GuiControls;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.GameStates
{
    public class MainMenuState : IGameState
    {
        private readonly Label _lblTitle;
        private readonly Button _btnPlay;

        private bool _startGame;

        private readonly GameState _gameState;

        public MainMenuState(GameState gameState)
        {
            _gameState = gameState;

            var fontArial = AssetsManager.Instance.GetSpriteFont("arialHeading");
            var fontLed = AssetsManager.Instance.GetSpriteFont("The Led Display St");

            string title = "SPACE SHOOTER";
            _lblTitle = new Label(fontLed, VerticalAlignment.Middle, HorizontalAlignment.Center,
                new Vector2(DeviceManager.Instance.ScreenWidth * 0.5f, DeviceManager.Instance.ScreenHeight * 0.2f),
                title, Color.Red) { TextShadow = true, TextShadowOffset = new Vector2(5.0f, 5.0f) };

            _btnPlay = new Button(fontArial, VerticalAlignment.Middle, HorizontalAlignment.Center,
                DeviceManager.Instance.ScreenDimensions * 0.5f, new Vector2(128.0f, 32.0f), string.Empty, Color.White, 1.0f, 1.0f, "sprBtnPlay",
                "sndBtn");
            _btnPlay.OnClick += btnPlay_Click;
        }

        public void Enter(IGameState previousGameState)
        {
            DeviceManager.Instance.IsMouseVisible = true;
        }

        public void Leave()
        {
            DeviceManager.Instance.IsMouseVisible = false;
        }

        public (IGameState currentGameState, IGameState newGameState) Update(GameTime gameTime)
        {
            _btnPlay.Update(gameTime);

            if (_startGame)
            {
                return (this, new GamePlayState(_gameState));
            }

            return (this, this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _lblTitle.Draw(spriteBatch);

            _btnPlay.Draw(spriteBatch);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            _startGame = true;
        }

        public IGameState Clone()
        {
            return this;
        }
    }
}