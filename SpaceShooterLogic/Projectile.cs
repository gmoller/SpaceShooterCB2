﻿using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic
{
    public class Projectile : Entity
    {
        public Projectile(string textureName, Vector2 position, Vector2 velocity)
        {
            Texture = AssetsManager.Instance.GetTexture(textureName);
            Scale = new Vector2(1.5f, 1.5f);
            SourceOrigin = new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f);
            DestinationOrigin = new Vector2(Texture.Width * 0.5f * Scale.X, Texture.Height * 0.5f * Scale.Y);
            Position = position;
            Body.Velocity = velocity;
            SetupBoundingBox(Texture.Width, Texture.Height);
        }
    }

    public class Projectiles : IEnumerable<Projectile>
    {
        private readonly List<Projectile> _projectiles = new List<Projectile>();

        public void Add(Projectile projectile)
        {
            _projectiles.Add(projectile);
        }

        public void Update(GameTime gameTime)
        {
            Movement(gameTime);
            CollisionDetectionWithEnemies();
        }

        private void Movement(GameTime gameTime)
        {
            for (int i = 0; i < _projectiles.Count; i++)
            {
                var projectile = _projectiles[i];
                projectile.Update(gameTime);
                if (projectile.Position.Y > DeviceManager.Instance.ScreenHeight || projectile.Position.Y < 0)
                {
                    _projectiles.Remove(projectile);
                }
            }
        }

        private void CollisionDetectionWithEnemies()
        {
            for (int i = 0; i < _projectiles.Count; i++)
            {
                Projectile projectile = _projectiles[i];
                if (GameEntitiesManager.Instance.Enemies.CollisionDetectionWithProjectile(projectile))
                {
                    _projectiles.Remove(projectile);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Projectile projectile in _projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }

        public IEnumerator<Projectile> GetEnumerator()
        {
            foreach (var item in _projectiles)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}