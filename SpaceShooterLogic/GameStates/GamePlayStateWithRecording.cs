using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.GameStates
{
    public class GamePlayStateWithRecording : GamePlayState
    {
        public override void Enter()
        {
            base.Enter();

            Recorder.Instance.StartRecording(1);
        }

        protected override void SetController()
        {
            InputComponent = new PlayerInputComponent(1);
        }

        public override void Leave()
        {
            base.Leave();

            Recorder.Instance.StopRecording();
        }
    }
}