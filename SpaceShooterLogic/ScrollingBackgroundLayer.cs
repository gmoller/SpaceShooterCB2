using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic
{
    public class ScrollingBackgroundLayer
    {
        private readonly Texture2D _texture;
        private readonly Vector2 _velocity;

        public Vector2 Position { get; set; } // center position of entity
        public int Depth { get; }
        public int PositionIndex { get; }
        public Vector2 InitialPosition { get; }

        public ScrollingBackgroundLayer(string textureName, int depth, int positionIndex, Vector2 position, Vector2 velocity)
        {
            _texture = AssetsManager.Instance.GetTexture(textureName);
            Depth = depth;
            PositionIndex = positionIndex;
            Position = position;
            InitialPosition = position;
            _velocity = velocity;
        }

        public void Update(GameTime gameTime)
        {
            Position = new Vector2(Position.X + _velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y + _velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    var destinationRectangle = DeviceManager.Instance.Viewport;
        //    spriteBatch.Draw(Texture, destinationRectangle, Color.White);
        //}
    }
}