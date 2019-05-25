using System.Collections.Generic;
using AnimationLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic
{
    public class Explosion
    {
        private Texture2D Texture { get; set; }
        public Vector2 Scale { get; set; } = new Vector2(1.5f, 1.5f);
        public Vector2 Position { get; set; } // center position of entity
        protected Vector2 SourceOrigin { get; set; } = Vector2.Zero;
        public Vector2 DestinationOrigin { get; set; } = Vector2.Zero;
        public PhysicsBody Body { get; }

        public AnimatedSprite Sprite { get; }

        public Explosion(string textureName, Vector2 position, Vector2 size)
        {
            Texture2D texture = AssetsManager.Instance.GetTexture(textureName);
            AnimationSpec animationSpec = AssetsManager.Instance.GetAnimations(textureName);

            Texture = texture;
            Sprite = new AnimatedSprite(animationSpec);
            Scale = new Vector2(size.X / Sprite.FrameWidth, size.Y / Sprite.FrameHeight) * 4.0f;

            SourceOrigin = new Vector2(Sprite.FrameWidth * 0.5f, Sprite.FrameHeight * 0.5f);
            DestinationOrigin = new Vector2(Sprite.FrameWidth * 0.5f * Scale.X, Sprite.FrameHeight * 0.5f * Scale.Y);
            Position = position;
            Body = new PhysicsBody();
            Body.Velocity = Vector2.Zero;
            SetupBoundingBox(Sprite.FrameWidth, Sprite.FrameHeight);
        }

        public void SetupBoundingBox(int width, int height)
        {
            Body.BoundingBox = new Rectangle(
                (int)(Position.X - (int)DestinationOrigin.X),
                (int)(Position.Y - (int)DestinationOrigin.Y),
                (int)(width * Scale.X),
                (int)(height * Scale.Y));
        }

        public void Update(GameTime gameTime)
        {
            Sprite.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var destRect = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)(Sprite.FrameWidth * Scale.X),
                (int)(Sprite.FrameHeight * Scale.Y));

             spriteBatch.Draw(Texture, destRect, Sprite.GetCurrentFrame(), Color.White, 0.0f, SourceOrigin, SpriteEffects.None, 0.0f);

             spriteBatch.DrawRectangle(Body.BoundingBox, Color.Magenta, 1.0f);
        }
    }

    public class Explosions
    {
        private readonly List<Explosion> _explosions = new List<Explosion>();

        public void Add(Explosion explosion)
        {
            _explosions.Add(explosion);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _explosions.Count; i++)
            {
                var explosion = _explosions[i];
                explosion.Update(gameTime);
                if (explosion.Sprite.IsFinished)
                {
                    _explosions.Remove(explosion);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Explosion explosion in _explosions)
            {
                explosion.Draw(spriteBatch);
            }
        }
    }
}