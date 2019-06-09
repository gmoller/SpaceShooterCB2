using GuiControls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Screens
{
    public class Metrics
    {
        private readonly GameState _gameState;

        private readonly Label _lblEntityCount;
        private readonly Label _lblAliveCount;

        public Metrics(GameState gameState)
        {
            _gameState = gameState;
            var font = AssetsManager.Instance.GetSpriteFont("arialTiny");

            // TODO: de-harcode positions
            _lblEntityCount = new Label(font, VerticalAlignment.Top, HorizontalAlignment.Left, new Vector2(0.0f, 600.0f), "Entity Count: ", Color.DarkGray) { TextShadow = false };
            _lblAliveCount = new Label(font, VerticalAlignment.Top, HorizontalAlignment.Left, new Vector2(0.0f, 620.0f), "Alive Count: ", Color.DarkGray) { TextShadow = false };
        }

        public void Update(GameTime gameTime)
        {
            _lblEntityCount.Text = $"Entity Count: {_gameState.EntityCount}";
            _lblAliveCount.Text = $"Alive Count: {_gameState.AliveEntityCount}";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _lblEntityCount.Draw(spriteBatch);
            _lblAliveCount.Draw(spriteBatch);
        }
    }
}