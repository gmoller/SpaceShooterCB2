using GameEngineCore;

namespace SpaceShooterLogic.Components
{
    public struct AnimationData
    {
        public AnimationSpec AnimationSpec { get; }
        public int CurrentFrame { get; set; }
        public float TimeSinceLastAnimationChange { get; set; }

        public AnimationData(AnimationSpec animationSpec, int currentFrame, float timeSinceLastAnimationChange)
        {
            AnimationSpec = animationSpec;
            CurrentFrame = currentFrame;
            TimeSinceLastAnimationChange = timeSinceLastAnimationChange;
        }

        public bool IsNull()
        {
            return AnimationSpec == null;
        }
    }
}