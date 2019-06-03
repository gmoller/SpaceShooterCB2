namespace SpaceShooterLogic.Systems
{
    public class AnimationSystem : System
    {
        // Components: AnimationData

        public AnimationSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOne(int entityId, float deltaTime)
        {
            #region gather data
            var animationData = GameState.AnimationData[entityId];
            #endregion

            #region process data
            if (animationData != null)
            {
                // is it time to change?
                var animData = animationData.Value;
                bool changeFrame = IsTimeToChangeFrame(animData.TimeSinceLastAnimationChange, animData.AnimationSpec.Duration);
                if (changeFrame)
                {
                    // if yes, change to next frame
                    int nextFrame;
                    if (animData.CurrentFrame < animData.AnimationSpec.NumberOfFrames - 1)
                    {
                        nextFrame = animData.CurrentFrame + 1;
                    }
                    else
                    {
                        if (animData.AnimationSpec.Repeating)
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
                    animData.CurrentFrame = nextFrame;
                    animData.TimeSinceLastAnimationChange = 0.0f;
                    GameState.AnimationData[entityId] = animData;

                    #endregion
                }
                else
                {
                    #region update data
                    animData.TimeSinceLastAnimationChange += deltaTime;
                    GameState.AnimationData[entityId] = animData;

                    #endregion
                }
            }
            #endregion
        }

        private bool IsTimeToChangeFrame(float timeElapsedSinceLastAnimationChange, int duration)
        {
            return !(timeElapsedSinceLastAnimationChange < duration);
        }
    }
}