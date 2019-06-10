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
}