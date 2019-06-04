using Microsoft.Xna.Framework;
using SpaceShooterLogic.Creators;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class FireProjectileSystem : System
    {
        private static readonly object Lock = new object();

        private readonly int _weaponCooldownTime; // in milliseconds
        private readonly Vector2 _weaponsOffset;
        private readonly Vector2 _weaponDirection;
        private readonly float _weaponVelocity;

        public FireProjectileSystem(string name, GameState gameState) : base(name, gameState)
        {
            // player laser settings (for now)
            _weaponCooldownTime = 200;
            _weaponsOffset = new Vector2(0.0f, -20.0f);
            _weaponDirection = new Vector2(0.0f, -1.0f);
            _weaponVelocity = 0.600f;
        }

        // TODO: store weapon fired time, and then check if current time - weaponfiredtime > cooldown time = fire weapon (no need to keep updating last time)
        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            #region gather data

            var firingEntityPosition = GameState.Positions[entityId];
            var timeElapsedSinceLastShot = GameState.TimesSinceLastShot[entityId];
            var shootWeapon = GameState.Tags[entityId].IsBitSet(1); // 1-player shoots

            #endregion

            if (firingEntityPosition.IsNull() || timeElapsedSinceLastShot.IsNegative()) return;

            #region process data

            // check if we are on cooldown
            var weaponOnCooldown = WeaponOnCooldown(timeElapsedSinceLastShot);
            if (weaponOnCooldown)
            {
                timeElapsedSinceLastShot += deltaTime;
            }

            // if not on cooldown and fire pressed, fire projectile
            if (!weaponOnCooldown && shootWeapon)
            {
                // create new projectile
                var sound = AssetsManager.Instance.GetSound("sndLaser");
                lock (Lock)
                {
                    sound.Play();
                }

                var projectilePosition = firingEntityPosition + _weaponsOffset;
                var projectileVelocity = _weaponDirection * _weaponVelocity;

                ProjectileCreator.Create2(_weaponDirection.Y < 0 ? "sprLaserPlayer" : "sprLaserEnemy0", projectilePosition, projectileVelocity, GameState);

                // put weapon on cooldown
                timeElapsedSinceLastShot = 0.0f;
            }

            #endregion

            #region update data

            GameState.Tags[entityId] = GameState.Tags[entityId].UnsetBit(1); // 1-player shoots
            GameState.TimesSinceLastShot[entityId] = timeElapsedSinceLastShot;

            #endregion
        }

        private bool WeaponOnCooldown(float timeElapsedSinceLastShot)
        {
            return timeElapsedSinceLastShot < _weaponCooldownTime;
        }
    }
}