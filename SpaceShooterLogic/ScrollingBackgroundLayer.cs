using Microsoft.Xna.Framework;
using SpaceShooterUtilities;

namespace SpaceShooterLogic
{
    public class ScrollingBackgroundLayer : Entity
    {
        public int Depth { get; }
        public int PositionIndex { get; }
        public Vector2 InitialPosition { get; }

        public ScrollingBackgroundLayer(string textureName, int depth, int positionIndex, Vector2 position, Vector2 velocity)
        {
            Texture = AssetsManager.Instance.GetTexture(textureName);
            Depth = depth;
            PositionIndex = positionIndex;
            Position = position;
            InitialPosition = position;
            Body.Velocity = velocity;
        }

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    var destinationRectangle = DeviceManager.Instance.Viewport;
        //    spriteBatch.Draw(Texture, destinationRectangle, Color.White);
        //}
    }
}