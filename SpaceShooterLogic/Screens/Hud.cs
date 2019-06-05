using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GuiControls;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Screens
{
    public class Hud
    {
        private readonly GameState _gameState;

        private readonly Label _lblScore;
        private readonly Label _lblLives;

        public Hud(GameState gameState)
        {
            _gameState = gameState;
            var font = AssetsManager.Instance.GetSpriteFont("arialSmall");
            _lblScore = new Label(font, VerticalAlignment.Top, HorizontalAlignment.Left, new Vector2(50.0f, 20.0f), "Score: ", Color.Red) { TextShadow = true };
            _lblLives = new Label(font, VerticalAlignment.Top, HorizontalAlignment.Right, new Vector2(DeviceManager.Instance.ScreenWidth - 50.0f, 20.0f), "Lives: ", Color.Red) { TextShadow = true };
        }

        public void Update(GameTime gameTime)
        {
            _lblScore.Text = $"Score: {_gameState.Score}";
            _lblLives.Text = $"Lives: {_gameState.Lives}";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _lblScore.Draw(spriteBatch);
            _lblLives.Draw(spriteBatch);
        }
    }
}