namespace SpaceShooterLogic.Components
{
    public struct Weapon
    {
        public bool MustShoot { get; set; }
        public float WeaponCooldownTime { get; set; } // in milliseconds
        public bool WeaponOnCooldown => WeaponCooldownTime > 0.0f;

        public Weapon(bool mustShoot)
        {
            MustShoot = mustShoot;
            WeaponCooldownTime = 0.0f;
        }
    }
}