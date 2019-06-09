namespace SpaceShooterLogic.Components
{
    public struct Enemy
    {
        public EnemyType Type { get; }
        public byte Score { get; }
        public float WeaponCooldownTime { get; set; } // in milliseconds
        public bool WeaponOnCooldown => WeaponCooldownTime > 0.0f;

        public Enemy(EnemyType type, byte score)
        {
            Type = type;
            Score = score;
            WeaponCooldownTime = 0.0f;
        }

        public bool IsNull()
        {
            return Type == EnemyType.None;
        }
    }

    public enum EnemyType : byte
    {
        None,
        Gunship,
        Chaser,
        Carrier
    }
}