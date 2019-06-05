//using System;
//using GameEngineCore;
//using GameEngineCore.AbstractClasses;
//using Microsoft.Xna.Framework;

//namespace SpaceShooterLogic.Components
//{
//    public class ShootingBehaviorComponent : UpdateComponent
//    {
//        public Vector2 Position { get; private set; }

//        public override void Update(float deltaTime)
//        {
//            Send(Position);
//        }

//        #region Send & Receive
//        private void Send(Vector2 position)
//        {
//            Communicator.Instance.Send(EntityId, typeof(LaserComponent), nameof(LaserComponent.ShootLaser), true);
//            Communicator.Instance.Send(EntityId, typeof(LaserComponent), nameof(LaserComponent.FiringEntityPosition), position);
//        }


//        public override void Receive(string attributeName, object payload)
//        {
//            switch (attributeName)
//            {
//                case "Position":
//                    Position = (Vector2)payload;
//                    break;
//                default:
//                    throw new NotSupportedException($"Attribute [{attributeName}] is not supported by ChasingBehaviorComponent.");
//            }
//        }
//        #endregion
//    }
//}