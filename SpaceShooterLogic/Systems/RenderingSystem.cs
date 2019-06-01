using GameEngineCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooterLogic.Systems
{
    public class RenderingSystem : System
    {
        public RenderingSystem(string name) : base(name)
        {
        }

        public SpriteBatch SpriteBatch { get; set; }

        protected override void ProcessOne(int entityId, float deltaTime)
        {
            #region gather data
            var texture = Registrar.Instance.GetComponent<Texture2D>("Textures", entityId);
            var position = Registrar.Instance.GetComponent<Vector2?>("Positions", entityId);
            var size = Registrar.Instance.GetComponent<Vector2?>("Sizes", entityId);
            var frame = Registrar.Instance.GetComponent<Rectangle?>("Frames", entityId);
            var rotation = Registrar.Instance.GetComponent<float?>("Rotations", entityId);
            #endregion

            #region process data
            if (texture != null && position != null && size != null && frame != null && rotation != null)
            {
                var frm = frame.Value;
                var sz = size.Value;
                var origin = new Vector2((int)(frm.Width * 0.5), (int)(frm.Height * 0.50));
                var scale = new Vector2(sz.X / frm.Width, sz.Y / frm.Height);

                SpriteBatch.Draw(texture, position.Value, frm, Color.White, rotation.Value, origin, scale, SpriteEffects.None, 0.0f);

                #region update data
                // no updates
                #endregion
            }
            #endregion
        }
    }

    // Components: Texture, Position, Size, Frame, Rotation
}