using System.Collections;
using System.Collections.Generic;
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

        public void Add(Texture2D texture, Vector2 position, Rectangle frame, float rotation, Vector2 origin, Vector2 scale)
        {
            var spriteBatchEntry = new SpriteBatchEntry(texture, position, frame, rotation, origin, scale);
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
        public Rectangle Frame { get; }
        public float Rotation { get; }
        public Vector2 Origin { get; }
        public Vector2 Scale { get; }


        public SpriteBatchEntry(Texture2D texture, Vector2 position, Rectangle frame, float rotation, Vector2 origin, Vector2 scale)
        {
            Texture = texture;
            Position = position;
            Frame = frame;
            Rotation = rotation;
            Origin = origin;
            Scale = scale;
        }
    }
}