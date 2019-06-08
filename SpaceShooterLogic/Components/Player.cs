namespace SpaceShooterLogic.Components
{
    public struct Player
    {
        public int Score { get; set; }
        public byte Lives { get; set; }
        public bool ShootAction { get; set; }

        public Player(int score, byte lives)
        {
            Score = score;
            Lives = lives;
            ShootAction = false;
        }

        public bool IsNull()
        {
            return Lives <= 0;
        }
    }
}