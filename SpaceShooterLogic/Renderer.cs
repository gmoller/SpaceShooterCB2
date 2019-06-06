using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooterLogic
{
    public class Renderer
    {
        public void Render(SpriteBatch spriteBatch, GameState gameState)
        {
            foreach (var spriteBatchEntry in gameState.SpriteBatchList)
            {
                spriteBatch.Draw(spriteBatchEntry.Texture, spriteBatchEntry.Position, spriteBatchEntry.Frame, Color.White, spriteBatchEntry.Rotation, spriteBatchEntry.Origin, spriteBatchEntry.Scale, SpriteEffects.None, 0.0f);
            }

            gameState.SpriteBatchList.Clear();
        }
    }
}