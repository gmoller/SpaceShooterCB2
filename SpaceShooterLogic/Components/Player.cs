namespace SpaceShooterLogic.Components
{
    public enum PlayerStatus : byte
    {
        Alive,
        Destroyed
    }

    public struct Player
    {
        public int Score { get; set; }
        public byte? Lives { get; set; }
        public PlayerStatus Status { get; set; }
        public float DeathCooldownTime { get; set; } // in milliseconds
        public bool DeathOnCooldown => DeathCooldownTime > 0.0f;

        public Player(int score, byte lives)
        {
            Score = score;
            Lives = lives;
            Status = PlayerStatus.Alive;
            DeathCooldownTime = 0.0f;
        }
    }
}