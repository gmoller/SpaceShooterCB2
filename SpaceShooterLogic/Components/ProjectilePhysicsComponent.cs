﻿using GameEngineCore;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SpaceShooterLogic.Creators;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    public class ProjectilePhysicsComponent : UpdateComponent
    {
        private const float EXPLOSION_SIZE_SCALE_FACTOR = 5.0f;

        private readonly Vector2 _velocity;

        public Vector2 Position { get; private set; }
        public Rectangle Volume { get; private set; }
        public Vector2 Size => new Vector2(Volume.Width, Volume.Height);

        internal ProjectilePhysicsComponent(Vector2 position, Vector2 velocity, Vector2 size)
        {
            Position = position;
            _velocity = velocity;
            Volume = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            DetermineBoundingBox();
        }

        public override void Update(float deltaTime)
        {
            // movement
            Position = Position + _velocity * deltaTime;
            DetermineBoundingBox();

            DetectCollisions();

            Send();
        }

        private void DetermineBoundingBox()
        {
            Vector2 origin = Size / 2.0f;

            Volume = new Rectangle(
                (int)(Position.X - (int)origin.X),
                (int)(Position.Y - (int)origin.Y),
                (int)Size.X,
                (int)Size.Y);
        }

        private void DetectCollisions()
        {
            // get things this entity can collide with
            Entities entities = Registrar.Instance.FilterEntities(Operator.Or, typeof(EnemyPhysicsComponent));

            foreach (ComponentsSet entity in entities)
            {
                if (EntityId == entity.EntityId) continue;

                // get physics component
                var physicsComponent = entity[typeof(EnemyPhysicsComponent)] as EnemyPhysicsComponent;

                // test for collision with this entity
                bool isColliding = Volume.Intersects(physicsComponent.Volume);
                if (isColliding)
                {
                    ResolveCollision(EntityId, Position, Size, entity.EntityId, physicsComponent.Position, physicsComponent.Size);
                    GameEntitiesManager.Instance.Score += physicsComponent.Score;
                    break;
                }
            }
        }

        private void ResolveCollision(int entity1Id, Vector2 entity1Position, Vector2 entity1Size,
                                      int entity2Id, Vector2 entity2Position, Vector2 entity2Size)
        {
            ExplodeEntity(entity1Id, entity1Position, entity1Size, "Fireball02");
            ExplodeEntity(entity2Id, entity2Position, entity2Size * EXPLOSION_SIZE_SCALE_FACTOR, "Explosion10");
        }

        private void ExplodeEntity(int entityId, Vector2 position, Vector2 size, string textureName)
        {
            // TODO: move the sound playing into the explosion
            int i = RandomGenerator.Instance.GetRandomInt(0, 1);
            SoundEffect sndExplode = AssetsManager.Instance.GetSound($"sndExplode{i}");
            sndExplode.Play();

            ExplosionCreator.Create(textureName, position, size);

            Registrar.Instance.RemoveEntity(entityId);
        }

        #region Send & Receive
        private void Send()
        {
            Communicator.Instance.Send(EntityId, typeof(VolumeGraphicsComponent), nameof(VolumeGraphicsComponent.Volume), Volume);
            Communicator.Instance.Send(EntityId, typeof(GraphicsComponent), nameof(GraphicsComponent.Position), Position);
        }

        public override void Receive(string attributeName, object payload)
        {
        }
        #endregion
    }
}