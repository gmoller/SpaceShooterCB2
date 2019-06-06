namespace SpaceShooterLogic
{
    public class SoundEffectPlayer
    {
        public void Play(GameState gameState)
        {
            foreach (var soundEffect in gameState.SoundEffectList)
            {
                soundEffect.Play();
            }

            gameState.SoundEffectList.Clear();
        }
    }
}