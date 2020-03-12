using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Components
{
    public struct Transform
    {
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public Vector2 Size { get; set; }

        public Transform(Vector2 position, Color color, float rotation, Vector2 scale, Vector2 size)
        {
            Position = position;
            Color = color;
            Rotation = rotation;
            Scale = scale;
            Size = size;
        }
    }
}