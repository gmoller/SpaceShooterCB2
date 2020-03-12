namespace SpaceShooterLogic.Components
{
    public enum EnemyType : byte
    {
        Gunship,
        Chaser,
        Carrier
    }

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
}