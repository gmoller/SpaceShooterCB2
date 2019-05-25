using System;
using AnimationLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    public abstract class GraphicsComponent : Component
    {
        public abstract void Draw(SpriteBatch spriteBatch);

        protected GraphicsComponent(int entityId) : base(entityId)
        {
        }
    }

    internal class PlayerGraphicsComponent : GraphicsComponent
    {
        private readonly Texture2D _texture;
        private readonly AnimatedSprite _sprite;
        private Vector2 _position;
        private Rectangle _volume;
        private Vector2 _size;

        internal PlayerGraphicsComponent(int entityId, string textureName) : base(entityId)
        {
            _texture = AssetsManager.Instance.GetTexture(textureName);
            AnimationSpec animationSpec = AssetsManager.Instance.GetAnimations(textureName);
            _sprite = new AnimatedSprite(animationSpec);
        }

        public override void Update(GameTime gameTime)
        {
            _sprite.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public override void Draw(SpriteBatch spriteBatch)
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
        public void Send()
        {
        }

        public override void Receive(AttributeType attributeId, object payload)
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
        #endregion
    }
}