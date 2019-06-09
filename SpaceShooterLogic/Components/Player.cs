namespace SpaceShooterLogic.Components
{
    public struct Player
    {
        public int Score { get; set; }
        public byte? Lives { get; set; }
        public bool ShootAction { get; set; }
        public PlayerStatus Status { get; set; }
        public float WeaponCooldownTime { get; set; } // in milliseconds
        public bool WeaponOnCooldown => WeaponCooldownTime > 0.0f;
        public float DeathCooldownTime { get; set; } // in milliseconds
        public bool DeathOnCooldown => DeathCooldownTime > 0.0f;

        public Player(int score, byte lives)
        {
            Score = score;
            Lives = lives;
            ShootAction = false;
            Status = PlayerStatus.Alive;
            WeaponCooldownTime = 0.0f;
            DeathCooldownTime = 0.0f;
        }

        public bool IsNull()
        {
            return Lives == null;
        }
    }

    public enum PlayerStatus : byte
    {
        Alive,
        Destroyed
    }
}