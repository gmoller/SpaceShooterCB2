using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooterLogic.Systems
{
    public class RenderingSystem : System
    {
        // Components: Texture, Position, Size, AnimationData

        private static readonly object Lock = new object();

        public RenderingSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        public SpriteBatch SpriteBatch { get; set; }

        protected override void ProcessOne(int entityId, float deltaTime)
        {
            #region gather data
            var texture = GameState.Textures[entityId];
            var position = GameState.Positions[entityId];
            var size = GameState.Sizes[entityId];
            var animationData = GameState.AnimationData[entityId];
            var rotation = GameState.Rotations[entityId];
            #endregion

            #region process data
            if (texture != null && position != null && size != null)
            {
                var sz = size.Value;

                Rectangle frame;
                if (animationData == null)
                {
                    frame = new Rectangle(0, 0, (int)sz.X, (int)sz.Y);
                }
                else
                {
                    frame = animationData.Value.AnimationSpec.Frames[animationData.Value.CurrentFrame];
                }

                var origin = new Vector2((int)(frame.Width * 0.5), (int)(frame.Height * 0.50));
                var scale = new Vector2(sz.X / frame.Width, sz.Y / frame.Height);
                var rot = rotation ?? 0.0f;

                lock (Lock)
                {
                    SpriteBatch.Draw(texture, position.Value, frame, Color.White, rot, origin, scale, SpriteEffects.None, 0.0f);
                }

                #region update data
                // no updates
                #endregion
            }
            #endregion
        }
    }
}