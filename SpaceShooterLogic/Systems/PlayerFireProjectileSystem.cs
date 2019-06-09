using Microsoft.Xna.Framework;
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
            // player laser settings
            _weaponCooldownTime = 200;
            _weaponsOffset = new Vector2(0.0f, -50.0f);
            _weaponDirection = new Vector2(0.0f, -1.0f);
            _weaponVelocity = 0.6f;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var player = GameState.Players[entityId];
            var firingEntityPosition = GameState.Positions[entityId];

            // selection
            if (player.IsNull() || firingEntityPosition.IsNull()) return;

            // process data
            // check if we are on cooldown
            var weaponOnCooldown = player.WeaponOnCooldown;
            if (weaponOnCooldown)
            {
                player.WeaponCooldownTime -= deltaTime;
                player.WeaponCooldownTime = MathHelper.Clamp(player.WeaponCooldownTime, 0.0f, _weaponCooldownTime);
            }
            else // if not on cooldown and fire pressed, fire projectile
            {
                if (player.ShootAction)
                {
                    // create new projectile
                    var projectilePosition = firingEntityPosition + _weaponsOffset;
                    var projectileVelocity = _weaponDirection * _weaponVelocity;
                    ProjectileCreator.Create("sprLaserPlayer", projectilePosition, projectileVelocity, GameState);

                    // put weapon on cooldown
                    player.WeaponCooldownTime = _weaponCooldownTime;
                    player.ShootAction = false;
                }
            }

            // update data
            GameState.Players[entityId] = player;
        }
    }
}