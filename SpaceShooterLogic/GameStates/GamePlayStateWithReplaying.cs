using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.GameStates
{
    public class GamePlayStateWithReplaying : GamePlayState
    {
        public override void Enter(IGameState previousGameState)
        {
            base.Enter(previousGameState);

            Replayer.Instance.ReadInData();
        }

        protected override void SetController()
        {
            InputComponent = new PlayerInputComponent();
        }
    }
}