//using System;
//using GameEngineCore.AbstractClasses;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using SpaceShooterUtilities;

//namespace SpaceShooterLogic.Components
//{
//    internal class VolumeGraphicsComponent : DrawComponent
//    {
//        public Rectangle Volume { get; private set; }

//        internal VolumeGraphicsComponent(Rectangle volume)
//        {
//            Volume = volume;
//        }

//        public override void Draw(SpriteBatch spriteBatch)
//        {
//            spriteBatch.DrawRectangle(Volume, Color.Red, 1.0f);
//        }

//        #region Send & Receive
//        private void Send()
//        {
//        }

//        public override void Receive(string attributeName, object payload)
//        {
//            switch (attributeName)
//            {
//                case "Volume":
//                    Volume = (Rectangle)payload;
//                    break;
//                default:
//                    throw new NotSupportedException($"Attribute [{attributeName}] is not supported by VolumeGraphicsComponent.");
//            }
//        }
//        #endregion
//    }
//}