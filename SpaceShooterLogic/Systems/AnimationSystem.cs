namespace SpaceShooterLogic.Systems
{
    public class AnimationSystem : System
    {
        public AnimationSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var animationData = GameState.AnimationData[entityId];

            // selection
            if (animationData == null) return;

            // process data
            // is it time to change?
            var ad = animationData.Value;
            bool changeFrame = IsTimeToChangeFrame(ad.FrameCooldownTime);
            if (changeFrame)
            {
                // if yes, change to next frame
                int nextFrame;
                if (ad.CurrentFrame < ad.Spec.NumberOfFrames - 1)
                {
                    nextFrame = ad.CurrentFrame + 1;
                }
                else
                {
                    if (ad.Spec.Repeating)
                    {
                        nextFrame = 0;
                    }
                    else
                    {
                        nextFrame = -1;
                        GameState.Tags[entityId] = 0;
                    }
                }

                // update data
                ad.CurrentFrame = nextFrame;
                ad.FrameCooldownTime = ad.Spec.Duration;
                GameState.AnimationData[entityId] = ad;
            }
            else
            {
                // update data
                ad.FrameCooldownTime -= deltaTime;
                GameState.AnimationData[entityId] = ad;
            }
        }

        private bool IsTimeToChangeFrame(float frameCooldownTime)
        {
            return frameCooldownTime <= 0.0f;
        }
    }
}