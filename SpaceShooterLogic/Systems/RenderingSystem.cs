using GameEngineCore;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;

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
            var transform = GameState.GameData.Transforms[entityId];
            var texture = GameState.GameData.Textures[entityId];

            // selection
            if (transform == null || texture == null) return;

            // gather data for processing
            var animationData = GameState.GameData.AnimationData[entityId];
            var volume = GameState.GameData.Volumes[entityId];

            // process data
            RectangleF frame = CalculateSourceRectangle(animationData.GetValueOrDefault(), texture.Width, texture.Height);

            var origin = new Vector2((int)(frame.Width * 0.5f), (int)(frame.Height * 0.5f));

            var t = transform.Value;
            GameState.AddToSpriteBatchList(texture, t.Position, frame, t.Rotation, origin, t.Scale, volume.GetValueOrDefault());

            // update data
            // no updates
        }

        private RectangleF CalculateSourceRectangle(AnimationData animationData, int textureWidth, int textureHeight)
        {
            RectangleF frame;
            if (animationData.Spec == null)
            {
                frame = new RectangleF(0, 0, textureWidth, textureHeight);
            }
            else
            {
                frame = animationData.Spec.Frames[animationData.CurrentFrame];
            }

            return frame;
        }
    }
}