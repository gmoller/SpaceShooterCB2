using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;
using SpaceShooterLogic.Creators;

namespace SpaceShooterLogic.Systems
{
    public class EnemyFireProjectileSystem : System
    {
        private readonly int _weaponCooldownTime; // in milliseconds
        private readonly Vector2 _weaponsOffset;
        private readonly Vector2 _weaponDirection;
        private readonly float _weaponVelocity;

        public EnemyFireProjectileSystem(string name, GameState gameState) : base(name, gameState)
        {
            // player laser settings (for now)
            _weaponCooldownTime = 1000;
            _weaponsOffset = new Vector2(0.0f, 50.0f);
            _weaponDirection = new Vector2(0.0f, 1.0f);
            _weaponVelocity = 0.3f;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var enemy = GameState.Enemies[entityId];
            var weapon = GameState.Weapons[entityId];

            // selection
            if (enemy == null || weapon == null) return;

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
                    ProjectileCreator.Create("sprLaserEnemy0", projectilePosition, projectileVelocity, GameState);

                    // put weapon on cooldown
                    w.WeaponCooldownTime = _weaponCooldownTime;
                }
            }

            // update data
            GameState.Weapons[entityId] = w;
        }
    }
}