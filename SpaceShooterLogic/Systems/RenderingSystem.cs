using Microsoft.Xna.Framework;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class RenderingSystem : System
    {
        public RenderingSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var texture = GameState.Textures[entityId];
            var position = GameState.Positions[entityId];
            var size = GameState.Sizes[entityId];

            // selection
            if (texture == null || position.IsNull() || size.IsNull()) return;

            // gather data for processing
            var animationData = GameState.AnimationData[entityId];
            var rotation = GameState.Rotations[entityId];
            var volume = GameState.Volumes[entityId];

            // process data
            var frame = animationData.AnimationSpec?.Frames[animationData.CurrentFrame] ?? new Rectangle(0, 0, (int)size.X, (int)size.Y);

            var origin = new Vector2((int)(frame.Width * 0.5), (int)(frame.Height * 0.50));
            var scale = new Vector2(size.X / frame.Width, size.Y / frame.Height);

            GameState.AddToSpriteBatchList(texture, position, frame, rotation, origin, scale, volume);

            // update data
            // no updates
        }
    }
}