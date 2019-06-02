using Microsoft.Xna.Framework;
using SpaceShooterLogic.Creators;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class FireProjectileSystem : System
    {
        // Components: Position, Tag-1

        private static readonly object Lock = new object();

        private readonly int _weaponCooldownTime; // in milliseconds
        private readonly Vector2 _weaponsOffset;
        private readonly Vector2 _weaponDirection;
        private readonly float _weaponVelocity;

        private float _timeElapsedSinceLastShot; // in milliseconds

        public FireProjectileSystem(string name, GameState gameState) : base(name, gameState)
        {
            // player laser settings (for now)
            _weaponCooldownTime = 200;
            _weaponsOffset = new Vector2(0.0f, -20.0f);
            _weaponDirection = new Vector2(0.0f, -1.0f);
            _weaponVelocity = 0.600f;
            _timeElapsedSinceLastShot = float.MaxValue; // to ensure we don't start on cooldown
        }

        protected override void ProcessOne(int entityId, float deltaTime)
        {
            #region gather data
            var firingEntityPosition = GameState.Positions[entityId];
            var shootLaser = GameState.Tags[entityId].IsBitSet(1); // 1-playershoots
            #endregion

            if (firingEntityPosition != null && shootLaser)
            {
                // check if we are on cooldown
                bool weaponOnCooldown = WeaponOnCooldown();
                if (weaponOnCooldown)
                {
                    _timeElapsedSinceLastShot += deltaTime;
                }

                // if not on cooldown
                if (!weaponOnCooldown)
                {
                    // create new projectile
                    var sound = AssetsManager.Instance.GetSound("sndLaser");
                    lock (Lock)
                    {
                        sound.Play();
                    }

                    Vector2 projectilePosition = firingEntityPosition.Value + _weaponsOffset;
                    Vector2 projectileVelocity = _weaponDirection * _weaponVelocity;

                    ProjectileCreator.Create2(_weaponDirection.Y < 0 ? "sprLaserPlayer" : "sprLaserEnemy0", projectilePosition, projectileVelocity, GameState);

                    // put weapon on cooldown
                    StartWeaponCooldown();
                }

                #region update data
                GameState.Tags[entityId] = GameState.Tags[entityId].UnsetBit(1); // 1-playershoots
                #endregion
            }
        }

        private bool WeaponOnCooldown()
        {
            return _timeElapsedSinceLastShot < _weaponCooldownTime;
        }

        private void StartWeaponCooldown()
        {
            _timeElapsedSinceLastShot = 0.0f;
        }
    }
}