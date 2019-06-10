using Microsoft.Xna.Framework;
using SpaceShooterLogic.Creators;

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
            var weapon = GameState.Weapons[entityId];

            // selection
            if (player == null || weapon == null) return;

            // process data
            // check if we are on cooldown
            var w = weapon.Value;
            var weaponOnCooldown = w.WeaponOnCooldown;
            if (weaponOnCooldown)
            {
                w.WeaponCooldownTime -= deltaTime;
                w.WeaponCooldownTime = MathHelper.Clamp(w.WeaponCooldownTime, 0.0f, _weaponCooldownTime);
            }
            else // if not on cooldown and must shoot, fire projectile
            {
                if (w.MustShoot)
                {
                    // create new projectile
                    var entityPosition = GameState.Transforms[entityId].Value.Position;
                    var projectilePosition = entityPosition + _weaponsOffset;
                    var projectileVelocity = _weaponDirection * _weaponVelocity;
                    ProjectileCreator.Create("sprLaserPlayer", projectilePosition, projectileVelocity, GameState);

                    // put weapon on cooldown
                    w.WeaponCooldownTime = _weaponCooldownTime;
                }
            }

            // update data
            GameState.Weapons[entityId] = w;
        }
    }
}