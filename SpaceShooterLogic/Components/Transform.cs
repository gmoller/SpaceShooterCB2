using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Components
{
    public struct Transform
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public Vector2 Size { get; set; }

        public Transform(Vector2 position, float rotation, Vector2 scale, Vector2 size)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Size = size;
        }
    }

    public struct Transform2 : IGameComponent
    {
        public Transform Transform { get; }
        public int EntityId { get; }

        public Transform2(int entityId, Vector2 position, float rotation, Vector2 scale, Vector2 size)
        {
            Transform = new Transform(position, rotation, scale, size);
            EntityId = entityId;
        }
    }

    public interface IGameComponent
    {
        int EntityId { get; }
    }
}