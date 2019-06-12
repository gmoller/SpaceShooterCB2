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
            if (spec == null)
                FrameCooldownTime = 0.0f;
            else
                FrameCooldownTime = spec.Duration;
        }
    }

    public struct AnimationData2 : IGameComponent
    {
        public AnimationData AnimationData { get; set; }
        public int EntityId { get; set; }

        public AnimationData2(int entityId, AnimationSpec spec)
        {
            AnimationData = new AnimationData(spec);
            EntityId = entityId;
        }
    }
}