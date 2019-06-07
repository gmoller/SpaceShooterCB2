namespace GameEngineCore
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
    }
}