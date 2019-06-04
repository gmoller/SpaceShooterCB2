using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class RenderingSystem : System
    {
        private static readonly object Lock = new object();

        public RenderingSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        public SpriteBatch SpriteBatch { get; set; }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            #region gather data

            var texture = GameState.Textures[entityId];
            var position = GameState.Positions[entityId];
            var size = GameState.Sizes[entityId];
            var animationData = GameState.AnimationData[entityId];
            var rotation = GameState.Rotations[entityId];

            #endregion

            if (texture == null || position.IsNull() || size.IsNull()) return;

            #region process data

            var frame = animationData.AnimationSpec?.Frames[animationData.CurrentFrame] ?? new Rectangle(0, 0, (int)size.X, (int)size.Y);

            var origin = new Vector2((int)(frame.Width * 0.5), (int)(frame.Height * 0.50));
            var scale = new Vector2(size.X / frame.Width, size.Y / frame.Height);

            lock (Lock)
            {
                SpriteBatch.Draw(texture, position, frame, Color.White, rotation, origin, scale, SpriteEffects.None, 0.0f);
            }

            #endregion

            #region update data

            // no updates

            #endregion
        }
    }
}