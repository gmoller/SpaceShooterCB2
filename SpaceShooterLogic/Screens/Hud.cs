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

        private readonly Label _lblEntityCount;
        private readonly Label _lblAliveCount;

        public Hud(GameState gameState)
        {
            _gameState = gameState;
            var font = AssetsManager.Instance.GetSpriteFont("arialSmall");
            _lblScore = new Label(font, VerticalAlignment.Top, HorizontalAlignment.Left, new Vector2(50.0f, 20.0f), "Score: ", Color.Red) { TextShadow = true };
            _lblLives = new Label(font, VerticalAlignment.Top, HorizontalAlignment.Right, new Vector2(DeviceManager.Instance.ScreenWidth - 50.0f, 20.0f), "Lives: ", Color.Red) { TextShadow = true };

            // TODO: de-harcode positions
            font = AssetsManager.Instance.GetSpriteFont("arialTiny");
            _lblEntityCount = new Label(font, VerticalAlignment.Top, HorizontalAlignment.Left, new Vector2(0.0f, 600.0f), "Entity Count: ", Color.DarkGray) { TextShadow = false };
            _lblAliveCount = new Label(font, VerticalAlignment.Top, HorizontalAlignment.Left, new Vector2(0.0f, 620.0f), "Alive Count: ", Color.DarkGray) { TextShadow = false };
        }

        public void Update(GameTime gameTime)
        {
            var p = _gameState.FindPlayer();
            _lblScore.Text = $"Score: {p.player.Score}";
            _lblLives.Text = $"Lives: {p.player.Lives}";

            _lblEntityCount.Text = $"Entity Count: {_gameState.EntityCount}";
            _lblAliveCount.Text = $"Alive Count: {_gameState.AliveEntityCount}";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _lblScore.Draw(spriteBatch);
            _lblLives.Draw(spriteBatch);

            _lblEntityCount.Draw(spriteBatch);
            _lblAliveCount.Draw(spriteBatch);
        }
    }
}