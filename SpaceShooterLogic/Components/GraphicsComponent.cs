using System;
using AnimationLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    public interface IGraphicsComponent : IComponent
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }

    internal class PlayerGraphicsComponent : IGraphicsComponent
    {
        private readonly Texture2D _texture;
        private readonly AnimatedSprite _sprite;
        private Vector2 _position;
        private Rectangle _volume;
        private Vector2 _size;

        internal PlayerGraphicsComponent(string textureName)
        {
            _texture = AssetsManager.Instance.GetTexture(textureName);
            AnimationSpec animationSpec = AssetsManager.Instance.GetAnimations(textureName);
            _sprite = new AnimatedSprite(animationSpec);
        }

        public void Update(GameTime gameTime)
        {
            _sprite.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var origin = new Vector2(_sprite.FrameWidth * 0.5f, _sprite.FrameHeight * 0.5f);
            //Vector2 origin = _size * 0.5f / 2.5f;

            var destRect = new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                (int)_size.X,
                (int)_size.Y);

            spriteBatch.Draw(_texture, destRect, _sprite.GetCurrentFrame(), Color.White, 0.0f, origin, SpriteEffects.None, 0.0f);

            spriteBatch.DrawRectangle(_volume, Color.Red, 1.0f);
        }

        #region Send & Receive
        public void Send(Player player)
        {
        }

        public void Receive(AttributeType attributeId, object payload)
        {
            switch (attributeId)
            {
                case AttributeType.GraphicsVolume:
                    _volume = (Rectangle)payload;
                    break;
                case AttributeType.GraphicsPosition:
                    _position = (Vector2)payload;
                    break;
                case AttributeType.GraphicsSize:
                    _size = (Vector2)payload;
                    break;
                default:
                    throw new NotSupportedException($"Attribute Id [{attributeId}] is not supported by PlayerGraphicsComponent.");
            }
        }

        public void Receive(AttributeType attributeId, Vector2 payload)
        {
            switch (attributeId)
            {
                case AttributeType.GraphicsPosition:
                    _position = payload;
                    break;
                case AttributeType.GraphicsSize:
                    _size = payload;
                    break;
                default:
                    throw new NotSupportedException($"Attribute Id [{attributeId}] of type [Vector2] is not supported by PlayerGraphicsComponent.");
            }
        }

        public void Receive(AttributeType attributeId, Rectangle payload)
        {
            switch (attributeId)
            {
                case AttributeType.GraphicsVolume:
                    _volume = payload;
                    break;
                default:
                    throw new NotSupportedException($"Attribute Id [{attributeId}] of type [Rectangle] is not supported by PlayerGraphicsComponent.");
            }
        }
        #endregion
    }
}