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

    public struct Enemy2 : IGameComponent
    {
        public Enemy Enemy { get; set; }
        public int EntityId { get; set; }

        public Enemy2(int entityId, EnemyType type, byte score)
        {
            Enemy = new Enemy(type, score);
            EntityId = entityId;
        }
    }
}