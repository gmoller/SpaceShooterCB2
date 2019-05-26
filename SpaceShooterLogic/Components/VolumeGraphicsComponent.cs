using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    internal class VolumeGraphicsComponent : DrawComponent
    {
        private Rectangle _volume;

        internal VolumeGraphicsComponent(Rectangle volume)
        {
            _volume = volume;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(_volume, Color.Red, 1.0f);
        }

        #region Send & Receive
        public override void Receive(AttributeType attributeId, object payload)
        {
            switch (attributeId)
            {
                case AttributeType.GraphicsVolume:
                    _volume = (Rectangle)payload;
                    break;
                default:
                    throw new NotSupportedException($"Attribute Id [{attributeId}] is not supported by VolumeGraphicsComponent.");
            }
        }
        #endregion
    }
}