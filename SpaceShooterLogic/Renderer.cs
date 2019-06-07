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
                var frm = new Rectangle((int)spriteBatchEntry.Frame.X, (int)spriteBatchEntry.Frame.Y, (int)spriteBatchEntry.Frame.Width, (int)spriteBatchEntry.Frame.Height);
                spriteBatch.Draw(spriteBatchEntry.Texture, spriteBatchEntry.Position, frm, Color.White, spriteBatchEntry.Rotation, spriteBatchEntry.Origin, spriteBatchEntry.Scale, SpriteEffects.None, 0.0f);

                //spriteBatch.DrawRectangle(spriteBatchEntry.Volume, Color.Green);
            }

            gameState.SpriteBatchList.Clear();
        }
    }
}