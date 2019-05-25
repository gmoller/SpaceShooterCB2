using System;
using Microsoft.Xna.Framework;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    public interface ILaserComponent : IComponent
    {
        void Update(Player player, GameTime gameTime);
    }

    internal class PlayerLaserComponent : ILaserComponent
    {
        private const int PLAYER_LASER_COOLDOWN = 200; // in milliseconds
        private const float PLAYER_LASER_VELOCITY = 600.0f; // in pixels per second

        private readonly Vector2 _laserOffsetFromPlayer = new Vector2(0.0f, -30.0f);

        private float _timeElapsedSinceLastPlayerShot; // in milliseconds

        internal PlayerLaserComponent()
        {
            _timeElapsedSinceLastPlayerShot = PLAYER_LASER_COOLDOWN;
        }

        public void Update(Player player, GameTime gameTime)
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
                var projectile = new Projectile(AssetsManager.Instance.GetTexture("sprLaserPlayer"), laserPosition, new Vector2(0, -PLAYER_LASER_VELOCITY));
                GameEntitiesManager.Instance.PlayerProjectiles.Add(projectile);

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
        public void Send(Player player)
        {
        }

        public void Receive(AttributeType attributeId, object payload)
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