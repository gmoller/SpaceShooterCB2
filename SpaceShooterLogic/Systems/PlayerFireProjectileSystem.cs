using Microsoft.Xna.Framework;
using GameEngineCore;
using SpaceShooterLogic.Creators;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class PlayerFireProjectileSystem : System
    {
        private readonly int _weaponCooldownTime; // in milliseconds
        private readonly Vector2 _weaponsOffset;
        private readonly Vector2 _weaponDirection;
        private readonly float _weaponVelocity;

        public PlayerFireProjectileSystem(string name, GameState gameState) : base(name, gameState)
        {
            // player laser settings (for now)
            _weaponCooldownTime = 200;
            _weaponsOffset = new Vector2(0.0f, -50.0f);
            _weaponDirection = new Vector2(0.0f, -1.0f);
            _weaponVelocity = 0.6f;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var firingEntityPosition = GameState.Positions[entityId];
            var timeElapsedSinceLastShot = GameState.TimesSinceLastShot[entityId];

            // selection
            if (firingEntityPosition.IsNull() || timeElapsedSinceLastShot.IsNegative()) return;

            // gather data for processing
            var player = GameState.Players[entityId];

            // process data
            // check if we are on cooldown
            var weaponOnCooldown = WeaponOnCooldown(timeElapsedSinceLastShot);
            if (weaponOnCooldown)
            {
                timeElapsedSinceLastShot += deltaTime;
            }

            // if not on cooldown and fire pressed, fire projectile
            if (!weaponOnCooldown && player.ShootAction)
            {
                // create new projectile
                var projectilePosition = firingEntityPosition + _weaponsOffset;
                var projectileVelocity = _weaponDirection * _weaponVelocity;
                ProjectileCreator.Create("sprLaserPlayer", projectilePosition, projectileVelocity, GameState);

                // put weapon on cooldown
                timeElapsedSinceLastShot = 0.0f;
                player.ShootAction = false;
            }

            // update data
            GameState.Players[entityId] = player;
            GameState.TimesSinceLastShot[entityId] = timeElapsedSinceLastShot;
        }

        private bool WeaponOnCooldown(float timeElapsedSinceLastShot)
        {
            return timeElapsedSinceLastShot < _weaponCooldownTime;
        }
    }
}