//using System;
//using GameEngineCore.AbstractClasses;
//using Microsoft.Xna.Framework;
//using SpaceShooterLogic.Creators;
//using SpaceShooterUtilities;

//namespace SpaceShooterLogic.Components
//{
//    internal class LaserComponent : UpdateComponent
//    {
//        private readonly Vector2 _laserDirection;
//        private readonly Vector2 _laserOffset;
//        private readonly float _laserVelocity;
//        private readonly int _laserCooldown;

//        private float _timeElapsedSinceLastShot; // in milliseconds

//        public bool ShootLaser { get; private set; }
//        public Vector2 FiringEntityPosition { get; private set; }

//        internal LaserComponent(Vector2 laserDirection, Vector2 laserOffset, float laserVelocity, int laserCooldown)
//        {
//            _laserDirection = laserDirection;
//            _laserOffset = laserOffset;
//            _laserVelocity = laserVelocity;
//            _laserCooldown = laserCooldown;

//            _timeElapsedSinceLastShot = laserCooldown;
//        }

//        public override void Update(float deltaTime)
//        {
//            if (_timeElapsedSinceLastShot < _laserCooldown)
//            {
//                _timeElapsedSinceLastShot += deltaTime;
//            }

//            if (ShootLaser)
//            {
//                Shoot(FiringEntityPosition);
//            }
//        }

//        private void Shoot(Vector2 firingEntityPosition)
//        {
//            ShootLaser = false;
//            if (!LaserOnCooldown())
//            {
//                AssetsManager.Instance.GetSound("sndLaser").Play();
//                Vector2 projectilePosition = firingEntityPosition + _laserOffset;
//                Vector2 projectileVelocity = _laserDirection * _laserVelocity;

//                //ProjectileCreator.Create(_laserDirection.Y < 0 ? "sprLaserPlayer": "sprLaserEnemy0", projectilePosition, projectileVelocity);

//                StartLaserCooldown();
//            }
//        }

//        private bool LaserOnCooldown()
//        {
//            if (_timeElapsedSinceLastShot < _laserCooldown)
//            {
//                return true;
//            }

//            return false;
//        }

//        private void StartLaserCooldown()
//        {
//            _timeElapsedSinceLastShot = 0.0f;
//        }

//        #region Send & Receive
//        private void Send()
//        {
//        }

//        public override void Receive(string attributeName, object payload)
//        {
//            switch (attributeName)
//            {
//                case "ShootLaser":
//                    ShootLaser = true;
//                    break;
//                case "FiringEntityPosition":
//                    FiringEntityPosition = (Vector2) payload;
//                    break;
//                default:
//                    throw new NotSupportedException($"Attribute [{attributeName}] is not supported by PlayerLaserComponent.");
//            }
//        }
//        #endregion
//    }
//}