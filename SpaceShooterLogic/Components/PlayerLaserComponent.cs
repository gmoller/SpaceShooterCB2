using System;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Creators;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    internal class PlayerLaserComponent : UpdateComponent
    {
        private const int PLAYER_LASER_COOLDOWN = 200; // in milliseconds
        private const float PLAYER_LASER_VELOCITY = 0.600f; // in pixels per millisecond

        private readonly Vector2 _laserOffsetFromPlayer;

        private float _timeElapsedSinceLastPlayerShot; // in milliseconds

        public bool ShootLaser { get; private set; }
        public Vector2 PlayerPosition { get; private set; }

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

            if (ShootLaser)
            {
                ShootLaser2(PlayerPosition);
            }
        }

        private void ShootLaser2(Vector2 playerPosition)
        {
            ShootLaser = false;
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

        public override void Receive(string attributeName, object payload)
        {
            switch (attributeName)
            {
                case "ShootLaser":
                    ShootLaser = true;
                    break;
                case "PlayerPosition":
                    PlayerPosition = (Vector2) payload;
                    break;
                default:
                    throw new NotSupportedException($"Attribute [{attributeName}] is not supported by PlayerLaserComponent.");
            }
        }
        #endregion
    }
}