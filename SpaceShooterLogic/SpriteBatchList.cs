using System.Collections;
using System.Collections.Generic;
using GameEngineCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooterLogic
{
    public class SpriteBatchList : IEnumerable<SpriteBatchEntry>
    {
        private readonly List<SpriteBatchEntry> _spriteBatchList;

        public SpriteBatchList()
        {
            _spriteBatchList = new List<SpriteBatchEntry>();
        }

        public void Add(Texture2D texture, Vector2 position, RectangleF frame, Color color, float rotation, Vector2 origin, Vector2 scale, Rectangle volume)
        {
            var spriteBatchEntry = new SpriteBatchEntry(texture, position, frame, color, rotation, origin, scale, volume);
            _spriteBatchList.Add(spriteBatchEntry);
        }

        public IEnumerator<SpriteBatchEntry> GetEnumerator()
        {
            foreach (var item in _spriteBatchList)
            {
                yield return item;
            }
        }

        public void Clear()
        {
            _spriteBatchList.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public struct SpriteBatchEntry
    {
        public Texture2D Texture { get; }
        public Vector2 Position { get; }
        public RectangleF Frame { get; }
        public Color Color { get; }
        public float Rotation { get; }
        public Vector2 Origin { get; }
        public Vector2 Scale { get; }
        public Rectangle Volume { get; }

        public SpriteBatchEntry(Texture2D texture, Vector2 position, RectangleF frame, Color color, float rotation, Vector2 origin, Vector2 scale, Rectangle volume)
        {
            Texture = texture;
            Position = position;
            Frame = frame;
            Color = color;
            Rotation = rotation;
            Origin = origin;
            Scale = scale;
            Volume = volume;
        }
    }
}