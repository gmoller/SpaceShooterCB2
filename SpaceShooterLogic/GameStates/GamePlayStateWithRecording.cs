using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.GameStates
{
    public class GamePlayStateWithRecording : GamePlayState
    {
        public override void Enter(IGameState previousGameState)
        {
            base.Enter(previousGameState);

            Recorder.Instance.StartRecording(1);
        }

        protected override void SetController()
        {
            InputComponent = new PlayerInputComponent();
        }

        public override void Leave()
        {
            base.Leave();

            Recorder.Instance.StopRecording();
        }
    }
}