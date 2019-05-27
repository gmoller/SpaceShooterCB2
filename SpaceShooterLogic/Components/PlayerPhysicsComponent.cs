using System;
using GameEngineCore;
using GameEngineCore.AbstractClasses;
using GameEngineCore.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SpaceShooterLogic.Creators;
using SpaceShooterLogic.Enemies;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    internal class PlayerPhysicsComponent : UpdateComponent
    {
        private const float MOVE_SPEED = 0.24f; // pixels per millisecond

        private Vector2 _position;
        private Rectangle _volume;
        private Vector2 Size => new Vector2(_volume.Width, _volume.Height);

        public Vector2 Velocity { get; private set; }

        internal PlayerPhysicsComponent(Vector2 position)
        {
            _position = position;
            _volume = new Rectangle(0, 0, 16, 16);
            DetermineBoundingBox();
        }

        public override void Update(float deltaTime)
        {
            // movement
            _position = _position + Velocity * deltaTime;
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

        private void DetermineBoundingBox()
        {
            Vector2 origin = Size / 2.0f;

            _volume = new Rectangle(
                (int)(_position.X - (int)origin.X),
                (int)(_position.Y - (int)origin.Y),
                (int)Size.X,
                (int)Size.Y);
        }

        private void ResolveCollisions()
        {
            // get enemies
            Entities entities = Registrar.Instance.FilterEntities(typeof(ProjectilePhysicsComponent));

            // for each enemy
            foreach (ComponentsSet entity in entities)
            {
                if (EntityId == entity.EntityId) continue;

                // get physics component
                IComponent component = entity[typeof(ProjectilePhysicsComponent)];
                var physicsComponent = component as ProjectilePhysicsComponent;

                // test for collision
                bool isColliding = _volume.Intersects(physicsComponent.Volume);
                if (isColliding)
                {
                    KillPlayer();
                    // kill enemy
                }
            }

            //Enemies.Enemies enemies = GameEntitiesManager.Instance.Enemies;
            //foreach (Enemy enemy in enemies)
            //{
            //    // check for enemy and player collision
            //    if (_volume.Intersects(enemy.Body.BoundingBox))
            //    {
            //        KillPlayer();
            //        enemy.KillEnemy();
            //    }
            //}

            //Projectiles projectiles = GameEntitiesManager.Instance.EnemyProjectiles;
            //foreach (Projectile projectile in projectiles)
            //{
            //    // check for player and enemy projectile collision
            //    if (_volume.Intersects(projectile.Body.BoundingBox))
            //    {
            //        KillPlayer();
            //    }
            //}
        }

        public void KillPlayer()
        {
            // TODO: move the sound playing into the explosion
            int i = RandomGenerator.Instance.GetRandomInt(0, 1);
            SoundEffect sndExplode = AssetsManager.Instance.GetSound($"sndExplode{i}");
            sndExplode.Play();

            ExplosionCreator.Create(_position, Size);

            Registrar.Instance.RemoveEntity(EntityId);
            GameEntitiesManager.Instance.PlayerIsDead = true;
        }

        #region Send & Receive
        public void Send()
        {
            Communicator.Instance.Send(EntityId, typeof(VolumeGraphicsComponent), nameof(VolumeGraphicsComponent.Volume), _volume);
            Communicator.Instance.Send(EntityId, typeof(GraphicsComponent), nameof(GraphicsComponent.Position), _position);
            Communicator.Instance.Send(EntityId, typeof(PlayerInputComponent), nameof(PlayerInputComponent.PlayerPosition), _position);
        }

        public override void Receive(string attributeName, object payload)
        {
            switch (attributeName)
            {
                case "Velocity":
                    Velocity = (Vector2)payload * MOVE_SPEED;
                    break;
                default:
                    throw new NotSupportedException($"Attribute [{attributeName}] is not supported by PlayerPhysicsComponent.");
            }
        }
        #endregion
    }
}