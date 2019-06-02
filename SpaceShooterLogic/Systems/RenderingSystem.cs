using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooterLogic.Systems
{
    public class RenderingSystem : System
    {
        // Components: Texture, Position, Size, Frame, Rotation

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
            var frame = GameState.Frames[entityId];
            var rotation = GameState.Rotations[entityId];
            #endregion

            #region process data
            if (texture != null && position != null && size != null && frame != null && rotation != null)
            {
                var frm = frame.Value;
                var sz = size.Value;
                var origin = new Vector2((int)(frm.Width * 0.5), (int)(frm.Height * 0.50));
                var scale = new Vector2(sz.X / frm.Width, sz.Y / frm.Height);

                lock (Lock)
                {
                    SpriteBatch.Draw(texture, position.Value, frm, Color.White, rotation.Value, origin, scale, SpriteEffects.None, 0.0f);
                }

                #region update data
                // no updates
                #endregion
            }
            #endregion
        }
    }
}