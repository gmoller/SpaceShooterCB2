using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooterLogic.Components
{
    public struct Texture2 : IGameComponent
    {
        public Texture2D Texture { get; set; }
        public int EntityId { get; set; }

        public Texture2(int entityId, Texture2D texture)
        {
            Texture = texture;
            EntityId = entityId;
        }
    }
}