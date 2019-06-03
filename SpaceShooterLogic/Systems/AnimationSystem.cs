namespace SpaceShooterLogic.Systems
{
    public class AnimationSystem : System
    {
        // Components: TimeSinceLastAnimationChange, AnimationSpec, CurrentFrame

        public AnimationSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOne(int entityId, float deltaTime)
        {
            #region gather data
            var timeElapsedSinceLastAnimationChange = GameState.TimesSinceLastAnimationChange[entityId];
            var animationSpec = GameState.AnimationSpecs[entityId];
            var currentFrame = GameState.CurrentFrames[entityId];
            #endregion

            #region process data
            if (timeElapsedSinceLastAnimationChange != null && animationSpec != null && currentFrame != null)
            {
                // is it time to change?
                bool changeFrame = IsTimeToChangeFrame(timeElapsedSinceLastAnimationChange.Value, animationSpec.Duration);
                if (changeFrame)
                {
                    // if yes, change to next frame
                    int nextFrame;
                    if (currentFrame < animationSpec.NumberOfFrames - 1)
                    {
                        nextFrame = currentFrame.Value + 1;
                    }
                    else
                    {
                        if (animationSpec.Repeating)
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
                    GameState.CurrentFrames[entityId] = nextFrame;
                    GameState.TimesSinceLastAnimationChange[entityId] = 0.0f;
                    #endregion
                }
                else
                {
                    #region update data
                    timeElapsedSinceLastAnimationChange += deltaTime;
                    GameState.TimesSinceLastAnimationChange[entityId] = timeElapsedSinceLastAnimationChange;
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