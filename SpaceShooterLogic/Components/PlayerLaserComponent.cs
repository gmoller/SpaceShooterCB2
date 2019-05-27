﻿using System;
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
            _laserOffsetFromPlayer = new Vector2(0.0f, -30.0f);
            _timeElapsedSinceLastPlayerShot = PLAYER_LASER_COOLDOWN;
        }

        public override void Update(GameTime gameTime)
        {
            if (_timeElapsedSinceLastPlayerShot < PLAYER_LASER_COOLDOWN)
            {
                _timeElapsedSinceLastPlayerShot += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        private void ShootLaser(Vector2 playerPosition)
        {
            if (!PlayerLaserOnCooldown())
            {
                AssetsManager.Instance.GetSound("sndLaser").Play();
                Vector2 laserPosition = playerPosition + _laserOffsetFromPlayer;
                Vector2 laserVelocity = new Vector2(0, -PLAYER_LASER_VELOCITY);

                //var projectile = new Projectile("sprLaserPlayer", laserPosition, laserVelocity);

                ProjectileCreator.Create(laserPosition, laserVelocity);
                //GameEntitiesManager.Instance.PlayerProjectiles.Add(projectile);

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