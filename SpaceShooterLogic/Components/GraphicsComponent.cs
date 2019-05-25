using AnimationLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    public interface IGraphicsComponent : IComponent
    {
        void Update(GameTime gameTime);
        void Draw(Player player, SpriteBatch spriteBatch);
    }

    internal class PlayerGraphicsComponent : IGraphicsComponent
    {
        private readonly Texture2D _texture;
        private readonly AnimatedSprite _sprite;

        internal PlayerGraphicsComponent(Texture2D texture)
        {
            _texture = texture;
            AnimationSpec animationSpec = AssetsManager.Instance.GetAnimations(texture.Name);
            _sprite = new AnimatedSprite(animationSpec);
        }

        public void Update(GameTime gameTime)
        {
            _sprite.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(Player player, SpriteBatch spriteBatch)
        {
            var origin = new Vector2(_sprite.FrameWidth * 0.5f, _sprite.FrameHeight * 0.5f);

            var destRect = new Rectangle(
                (int)player.Position.X,
                (int)player.Position.Y,
                (int)(_sprite.FrameWidth * player.Scale.X),
                (int)(_sprite.FrameHeight * player.Scale.Y));
            spriteBatch.Draw(_texture, destRect, _sprite.GetCurrentFrame(), Color.White, 0.0f, origin, SpriteEffects.None, 0.0f);

            spriteBatch.DrawRectangle(player.PhysicsBody.BoundingBox, Color.Red, 1.0f);
        }

        public void Receive(object payload)
        {
        }
    }
}