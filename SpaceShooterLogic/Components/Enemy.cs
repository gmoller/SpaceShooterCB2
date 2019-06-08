namespace SpaceShooterLogic.Components
{
    public struct Enemy
    {
        public EnemyType Type { get; }
        public byte Score { get; }

        public Enemy(EnemyType type, byte score)
        {
            Type = type;
            Score = score;
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