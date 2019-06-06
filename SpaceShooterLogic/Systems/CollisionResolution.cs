using GameEngineCore;
using SpaceShooterLogic.Creators;

namespace SpaceShooterLogic.Systems
{
    public class CollisionResolution : System
    {
        private readonly float _explosionSizeScaleFactor;

        public CollisionResolution(string name, GameState gameState) : base(name, gameState)
        {
            _explosionSizeScaleFactor = 5.0f;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var tag = GameState.Tags[entityId].IsBitSet((int)Tag.CollisionDetected);

            // selection
            if (!tag) return;

            // gather data for processing
            var position = GameState.Positions[entityId];
            var size = GameState.Sizes[entityId];
            var scoreToAdd = GameState.ScoreValues[entityId];

            // process data
            ExplosionCreator.Create("Explosion10", position, size * _explosionSizeScaleFactor, GameState);
            GameState.Score += scoreToAdd;

            // update data
            GameState.Tags[entityId] = GameState.Tags[entityId].UnsetBits((int)Tag.IsAlive, (int)Tag.CollisionDetected);
        }
    }
}