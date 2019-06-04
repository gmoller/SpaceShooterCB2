namespace SpaceShooterLogic.Systems
{
    public class AnimationSystem : System
    {
        public AnimationSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            #region gather data

            var animationData = GameState.AnimationData[entityId];

            #endregion

            if (animationData.AnimationSpec == null) return;

            #region process data

            // is it time to change?
            bool changeFrame = IsTimeToChangeFrame(animationData.TimeSinceLastAnimationChange, animationData.AnimationSpec.Duration);
            if (changeFrame)
            {
                // if yes, change to next frame
                int nextFrame;
                if (animationData.CurrentFrame < animationData.AnimationSpec.NumberOfFrames - 1)
                {
                    nextFrame = animationData.CurrentFrame + 1;
                }
                else
                {
                    if (animationData.AnimationSpec.Repeating)
                    {
                        nextFrame = 0;
                    }
                    else
                    {
                        nextFrame = -1; // TODO: properly support non-repeating animation specs
                        //isFinished = true;
                    }
                }

                #region update data

                animationData.CurrentFrame = nextFrame;
                animationData.TimeSinceLastAnimationChange = 0.0f;
                GameState.AnimationData[entityId] = animationData;

                #endregion
            }
            else
            {
                #region update data

                animationData.TimeSinceLastAnimationChange += deltaTime;
                GameState.AnimationData[entityId] = animationData;

                #endregion
            }

            #endregion
        }

        private bool IsTimeToChangeFrame(float timeElapsedSinceLastAnimationChange, int duration)
        {
            return !(timeElapsedSinceLastAnimationChange < duration);
        }
    }
}