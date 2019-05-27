using System;
using GameEngineCore;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Creators;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    internal class PlayerLaserComponent : UpdateComponent
    {
        private const int PLAYER_LASER_COOLDOWN = 200; // in milliseconds
        private const float PLAYER_LASER_VELOCITY = 600.0f; // in pixels per second

        private readonly Vector2 _laserOffsetFromPlayer;
        private float _timeElapsedSinceLastPlayerShot; // in milliseconds

        internal PlayerLaserComponent()
        {
            _laserOffsetFromPlayer = new Vector2(0.0f, -16.0f); // TODO: calculate this properly (based on size of player ship)
            _timeElapsedSinceLastPlayerShot = PLAYER_LASER_COOLDOWN;
        }

        public override void Update(float deltaTime)
        {
            if (_timeElapsedSinceLastPlayerShot < PLAYER_LASER_COOLDOWN)
            {
                _timeElapsedSinceLastPlayerShot += deltaTime;
            }
        }

        private void ShootLaser(Vector2 playerPosition)
        {
            if (!PlayerLaserOnCooldown())
            {
                AssetsManager.Instance.GetSound("sndLaser").Play();
                Vector2 projectilePosition = playerPosition + _laserOffsetFromPlayer;
                Vector2 projectileVelocity = new Vector2(0, -PLAYER_LASER_VELOCITY);

                ProjectileCreator.Create(projectilePosition, projectileVelocity);

                StartPlayerLaserCooldown();
            }
        }

        private bool PlayerLaserOnCooldown()
        {
            if (_timeElapsedSinceLastPlayerShot < PLAYER_LASER_COOLDOWN)
            {
                return true;
            }

            return false;
        }

        private void StartPlayerLaserCooldown()
        {
            _timeElapsedSinceLastPlayerShot = 0.0f;
        }

        #region Send & Receive
        public void Send()
        {
        }

        public override void Receive(AttributeType attributeId, object payload)
        {
            switch (attributeId)
            {
                case AttributeType.LaserShootLaser:
                    ShootLaser((Vector2)payload);
                    break;
                default:
                    throw new NotSupportedException($"Attribute Id [{attributeId}] is not supported by PlayerLaserComponent.");
            }
        }
        #endregion
    }
}