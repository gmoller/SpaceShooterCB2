using GameEngineCore;

namespace SpaceShooterLogic.Components
{
    public struct AnimationData
    {
        public AnimationSpec Spec { get; }
        public int CurrentFrame { get; set; }
        public float FrameCooldownTime { get; set; }

        public AnimationData(AnimationSpec spec)
        {
            Spec = spec;
            CurrentFrame = 0;
            FrameCooldownTime = spec.Duration;
        }
    }
}