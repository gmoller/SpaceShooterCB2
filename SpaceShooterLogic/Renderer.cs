using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic
{
    public class Renderer
    {
        public void Render(SpriteBatch spriteBatch, GameState gameState)
        {
            foreach (var spriteBatchEntry in gameState.SpriteBatchList)
            {
                var frame = new Rectangle((int)spriteBatchEntry.Frame.X, (int)spriteBatchEntry.Frame.Y, (int)spriteBatchEntry.Frame.Width, (int)spriteBatchEntry.Frame.Height);
                spriteBatch.Draw(spriteBatchEntry.Texture, spriteBatchEntry.Position, frame, spriteBatchEntry.Color, spriteBatchEntry.Rotation, spriteBatchEntry.Origin, spriteBatchEntry.Scale, SpriteEffects.None, 0.0f);

                //spriteBatch.DrawRectangle(spriteBatchEntry.Volume, Color.Green);
            }

            gameState.SpriteBatchList.Clear();
        }
    }
}