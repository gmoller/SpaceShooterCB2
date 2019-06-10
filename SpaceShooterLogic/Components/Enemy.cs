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
    }

    public enum EnemyType : byte
    {
        Gunship,
        Chaser,
        Carrier
    }
}