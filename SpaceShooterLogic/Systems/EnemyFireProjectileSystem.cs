using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;
using SpaceShooterLogic.Creators;
using SpaceShooterUtilities;

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
            var firingEntityPosition = GameState.Positions[entityId];

            // selection
            if (enemy.Type != EnemyType.Gunship || firingEntityPosition.IsNull()) return;

            // process data
            // check if we are on cooldown
            var weaponOnCooldown = enemy.WeaponOnCooldown;
            if (weaponOnCooldown)
            {
                enemy.WeaponCooldownTime -= deltaTime;
                enemy.WeaponCooldownTime = MathHelper.Clamp(enemy.WeaponCooldownTime, 0.0f, _weaponCooldownTime);
            }
            else // if not on cooldown, fire projectile
            {
                // create new projectile
                var projectilePosition = firingEntityPosition + _weaponsOffset;
                var projectileVelocity = _weaponDirection * _weaponVelocity;
                ProjectileCreator.Create("sprLaserEnemy0", projectilePosition, projectileVelocity, GameState);

                // put weapon on cooldown
                enemy.WeaponCooldownTime = _weaponCooldownTime;
            }

            // update data
            GameState.Enemies[entityId] = enemy;
        }
    }
}