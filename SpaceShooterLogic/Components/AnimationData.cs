using GameEngineCore;

namespace SpaceShooterLogic.Components
{
    public struct AnimationData
    {
        public AnimationSpec Spec { get; }
        public int CurrentFrame { get; set; }
        public float TimeSinceLastAnimationChange { get; set; }

        public AnimationData(AnimationSpec spec, int currentFrame, float timeSinceLastAnimationChange)
        {
            Spec = spec;
            CurrentFrame = currentFrame;
            TimeSinceLastAnimationChange = timeSinceLastAnimationChange;
        }
    }
}