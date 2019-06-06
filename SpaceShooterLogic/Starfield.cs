using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic
{
    public class Star
    {
        private readonly Texture2D _texture;
        private readonly Vector2 _velocity;
        private readonly Rectangle _sourceRectangle;

        public Vector2 Location { get; set; }
        public Color TintColor { get; set; }

        public Star(Vector2 location, Texture2D texture, Vector2 starSize, Vector2 velocity)
        {
            _texture = texture;
            _sourceRectangle = new Rectangle(0, 0, (int)starSize.X, (int)starSize.Y);
            _velocity = velocity;
            Location = location;
        }

        public Rectangle Destination => new Rectangle((int)Location.X, (int)Location.Y, _sourceRectangle.Width, _sourceRectangle.Height);

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Location += _velocity * elapsed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Destination, _sourceRectangle, TintColor);
        }
    }

    public class StarField
    {
        private readonly List<Star> _stars = new List<Star>();
        private readonly Random _rand = new Random();
        private readonly Color[] _colors = { Color.White, Color.Yellow, Color.Red };

        public StarField(int starCount, Vector2 starVelocity, Vector2 starSize)
        {
            var pixel = new Texture2D(DeviceManager.Instance.GraphicsDevice, (int)starSize.X, (int)starSize.Y, false, SurfaceFormat.Color);

            int numberOfPixels = (int)(starSize.X * starSize.Y);
            Color[] colorData = new Color[numberOfPixels];
            for (int i = 0; i < numberOfPixels; ++i)
            {
                colorData[i] = Color.White;
            }

            pixel.SetData(colorData);

            for (int i = 0; i < starCount; ++i)
            {
                Vector2 starVel = i % 2 == 0 ? starVelocity : starVelocity * 2.0f;
                _stars.Add(new Star(new Vector2(_rand.Next(0, DeviceManager.Instance.ScreenWidth), _rand.Next(0, DeviceManager.Instance.ScreenHeight)), pixel, starSize, starVel));
            }

            foreach (var star in _stars)
            {
                star.TintColor = _colors[_rand.Next(0, _colors.Length)];
                star.TintColor *= _rand.Next(30, 80) / 100.0f;
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var star in _stars)
            {
                star.Update(gameTime);
                if (star.Location.Y > DeviceManager.Instance.ScreenHeight)
                {
                    star.Location = new Vector2(_rand.Next(0, DeviceManager.Instance.ScreenWidth), 0.0f);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var star in _stars)
            {
                star.Draw(spriteBatch);
            }
        }
    }
}