using System;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    internal class GraphicsComponent : DrawComponent
    {
        private readonly Texture2D _texture;

        public Vector2 Position { get; private set; }
        public Rectangle Frame { get; private set; }

        internal GraphicsComponent(string textureName, Vector2 position)
        {
            _texture = AssetsManager.Instance.GetTexture(textureName);
            Position = position;
            Frame = new Rectangle();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2((int)(Frame.Width * 0.5), (int)(Frame.Height * 0.50)); // TODO: if size does not change do we need to keep calculating this?

            spriteBatch.Draw(_texture, Position, Frame, Color.White, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f); // TODO: use scale (2.5f)
        }

        #region Send & Receive
        public void Send()
        {
        }

        public override void Receive(string attributeName, object payload)
        {
            switch (attributeName)
            {
                case "Position":
                    Position = (Vector2)payload;
                    break;
                case "Frame":
                    Frame = (Rectangle)payload;
                    break;
                default:
                    throw new NotSupportedException($"Attribute [{attributeName}] is not supported by GraphicsComponent.");
            }
        }
        #endregion
    }
}