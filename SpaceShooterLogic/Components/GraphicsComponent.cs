using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    internal class GraphicsComponent : DrawComponent
    {
        private readonly Texture2D _texture;
        private Vector2 _position;
        private Rectangle _frame;

        internal GraphicsComponent(string textureName, Vector2 position)
        {
            _texture = AssetsManager.Instance.GetTexture(textureName);
            _position = position;
            _frame = new Rectangle();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2((int)(_frame.Width * 0.5), (int)(_frame.Height * 0.50)); // TODO: if size does not change do we need to keep calculating this?

            spriteBatch.Draw(_texture, _position, _frame, Color.White, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f); // TODO: use scale (2.5f)
        }

        #region Send & Receive
        public void Send()
        {
        }

        public override void Receive(AttributeType attributeId, object payload)
        {
            switch (attributeId)
            {
                case AttributeType.GraphicsPosition:
                    _position = (Vector2)payload;
                    break;
                case AttributeType.GraphicsFrame:
                    _frame = (Rectangle)payload;
                    break;
                default:
                    throw new NotSupportedException($"Attribute Id [{attributeId}] is not supported by GraphicsComponent.");
            }
        }
        #endregion
    }
}