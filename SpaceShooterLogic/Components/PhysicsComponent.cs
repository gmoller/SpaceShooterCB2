using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SpaceShooterLogic.Enemies;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    internal class PlayerPhysicsComponent : Component
    {
        private const float MOVE_SPEED = 240.0f; // pixels per second

        private Vector2 _position;
        private Vector2 _velocity;
        private Rectangle _volume;
        private Vector2 Size => new Vector2(_volume.Width, _volume.Height);

        internal PlayerPhysicsComponent(int entityId, Vector2 position) : base(entityId)
        {
            _position = position;
            _volume = new Rectangle(0, 0, (int)(16 * 2.5f), (int)(16 * 2.5f));
            DetermineBoundingBox();
        }

        private void DetermineBoundingBox()
        {
            Vector2 origin = Size / 2.0f;

            _volume = new Rectangle(
                (int)(_position.X - (int)origin.X),
                (int)(_position.Y - (int)origin.Y),
                (int)Size.X,
                (int)Size.Y);
        }

        public override void Update(GameTime gameTime)
        {
            // movement
            _position = _position + _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            DetermineBoundingBox();
            
            // do not allow our player off the screen
            float x = Size.X / 2.0f;
            float y = Size.Y / 2.0f;
            _position = new Vector2(
                MathHelper.Clamp(_position.X, x, DeviceManager.Instance.ScreenWidth - x),
                MathHelper.Clamp(_position.Y, y, DeviceManager.Instance.ScreenHeight - y));

            ResolveCollisions();

            Send();
        }

        private void ResolveCollisions()
        {
            Enemies.Enemies enemies = GameEntitiesManager.Instance.Enemies;
            foreach (Enemy enemy in enemies)
            {
                // check for enemy and player collision
                if (_volume.Intersects(enemy.Body.BoundingBox))
                {
                    KillPlayer();
                    enemy.KillEnemy();
                }
            }

            Projectiles projectiles = GameEntitiesManager.Instance.EnemyProjectiles;
            foreach (Projectile projectile in projectiles)
            {
                // check for player and enemy projectile collision
                if (_volume.Intersects(projectile.Body.BoundingBox))
                {
                    KillPlayer();
                }
            }
        }

        public void KillPlayer()
        {
            int i = RandomGenerator.Instance.GetRandomInt(0, 1);
            SoundEffect sndExplode = AssetsManager.Instance.GetSound($"sndExplode{i}");
            sndExplode.Play();

            var explosion = new Explosion("Fireball02", _position, Size);
            GameEntitiesManager.Instance.Explosions.Add(explosion);

            Registrar.Instance.Remove(1);
            GameEntitiesManager.Instance.PlayerIsDead = true;
        }

        #region Send & Receive
        public void Send()
        {
            Communicator.Instance.Send(EntityId, ComponentType.Graphics, AttributeType.GraphicsVolume, _volume);
            Communicator.Instance.Send(EntityId, ComponentType.Graphics, AttributeType.GraphicsSize, Size);
            Communicator.Instance.Send(EntityId, ComponentType.Graphics, AttributeType.GraphicsPosition, _position);
            Communicator.Instance.Send(EntityId, ComponentType.Input, AttributeType.InputPlayerPosition, _position);
        }

        public override void Receive(AttributeType attributeId, object payload)
        {
            switch (attributeId)
            {
                case AttributeType.PhysicsVelocity:
                    _velocity = (Vector2)payload * MOVE_SPEED;
                    break;
                default:
                    throw new NotSupportedException($"Attribute Id [{attributeId}] is not supported by PlayerPhysicsComponent.");
            }
        }
        #endregion
    }
}