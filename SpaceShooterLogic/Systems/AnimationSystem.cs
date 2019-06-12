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
            var animationData = GameState.GameData.AnimationData[entityId];

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
                    nextFrame = 0;
                    if (!ad.Spec.Repeating)
                    {
                        GameState.GameData.Tags[entityId] = 0;
                    }
                }

                // update data
                ad.CurrentFrame = nextFrame;
                ad.FrameCooldownTime = ad.Spec.Duration;
                GameState.GameData.AnimationData[entityId] = ad;
            }
            else
            {
                // update data
                ad.FrameCooldownTime -= deltaTime;
                GameState.GameData.AnimationData[entityId] = ad;
            }
        }

        private bool IsTimeToChangeFrame(float frameCooldownTime)
        {
            return frameCooldownTime <= 0.0f;
        }
    }
}