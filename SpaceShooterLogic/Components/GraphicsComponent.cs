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
        public Vector2 Size { get; private set; }
        public float Rotation { get; private set; }
        public Rectangle Frame { get; private set; }

        internal GraphicsComponent(string textureName, Vector2 position, Vector2 size)
        {
            _texture = AssetsManager.Instance.GetTexture(textureName);
            Position = position;
            Size = size;
            Rotation = 0.0f;
            Frame = new Rectangle(0, 0, (int)Size.X, (int)Size.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var origin = new Vector2((int)(Frame.Width * 0.5), (int)(Frame.Height * 0.50));
            var scale = new Vector2(Size.X / Frame.Width, Size.Y / Frame.Height);

            spriteBatch.Draw(_texture, Position, Frame, Color.White, Rotation, origin, scale, SpriteEffects.None, 0.0f);
        }

        #region Send & Receive
        private void Send()
        {
        }

        public override void Receive(string attributeName, object payload)
        {
            switch (attributeName)
            {
                case "Position":
                    Position = (Vector2)payload;
                    break;
                case "Size":
                    Size = (Vector2)payload;
                    break;
                case "Frame":
                    Frame = (Rectangle)payload;
                    break;
                case "Rotation":
                    Rotation = (float)payload;
                    break;
                default:
                    throw new NotSupportedException($"Attribute [{attributeName}] is not supported by GraphicsComponent.");
            }
        }
        #endregion
    }
}