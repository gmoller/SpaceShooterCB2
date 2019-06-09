using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using GameEngineCore;
using SpaceShooterLogic.Components;
using SpaceShooterLogic.Screens;

namespace SpaceShooterLogic
{
    public class GameState
    {
        public bool GameOver { get; set; }

        public Hud Hud { get; }
        public Metrics QMetrics { get; }

        public int EntityCount { get; set; }
        public int AliveEntityCount { get; set; }
        public float AlivePercentage => AliveEntityCount / (float)EntityCount * 100;

        #region Component Type Arrays

        // standard entity
        public Bag<Vector2> Positions { get; private set; }
        public Bag<Vector2> Velocities { get; private set; }
        public Bag<Rectangle> Volumes { get; private set; }
        public Bag<Texture2D> Textures { get; private set; }
        public Bag<Vector2> Sizes { get; private set; }

        public Bag<float> Rotations { get; private set; }

        public Bag<float> TimesSinceLastShot { get; private set; }
        public Bag<float> TimesSinceLastEnemySpawned { get; private set; }

        // animations
        public Bag<AnimationData> AnimationData { get; private set; }

        public Bag<Player> Players { get; private set; }
        public Bag<Enemy> Enemies { get; private set; }

        public Bag<byte> Tags { get; private set; }

        #endregion

        public SpriteBatchList SpriteBatchList { get; }
        public SoundEffectList SoundEffectList { get; }

        public BenchmarkMetrics Metrics { get; }

        public GameState()
        {
            Metrics = new BenchmarkMetrics();
            SpriteBatchList = new SpriteBatchList();
            SoundEffectList = new SoundEffectList();
            ClearState();

            Hud = new Hud(this);
            QMetrics = new Metrics(this);
        }

        public void AddToSpriteBatchList(Texture2D texture, Vector2 position, RectangleF frame, float rotation, Vector2 origin, Vector2 scale, Rectangle volume)
        {
            SpriteBatchList.Add(texture, position, frame, rotation, origin, scale, volume);
        }

        public void AddToSoundEffectList(SoundEffect sound)
        {
            SoundEffectList.Add(sound);
        }

        public void ClearState()
        {
            EntityCount = 0;
            AliveEntityCount = 0;

            Positions = new Bag<Vector2>();
            Velocities = new Bag<Vector2>();
            Volumes = new Bag<Rectangle>();
            Textures = new Bag<Texture2D>();
            Sizes = new Bag<Vector2>();
            Rotations = new Bag<float>();
            TimesSinceLastShot = new Bag<float>();
            TimesSinceLastEnemySpawned = new Bag<float>();
            AnimationData = new Bag<AnimationData>();
            Players = new Bag<Player>();
            Enemies = new Bag<Enemy>();
            Tags = new Bag<byte>();
        }

        public (Player player, int index) FindPlayer()
        {
            for (int i = 0; i < EntityCount - 1; ++i)
            {
                var player = Players[i];
                if (!player.IsNull())
                {
                    return (player, i);
                }
            }

            throw new Exception("Player not found!");
        }

        public void CompactEntities()
        {
            var newPositions = new Bag<Vector2>();
            var newVelocities = new Bag<Vector2>();
            var newVolumes = new Bag<Rectangle>();
            var newTextures = new Bag<Texture2D>();
            var newSizes = new Bag<Vector2>();
            var newRotations = new Bag<float>();
            var newTimesSinceLastShot = new Bag<float>();
            var newTimesSinceLastEnemySpawned = new Bag<float>();
            var newAnimationData = new Bag<AnimationData>();
            var newPlayers = new Bag<Player>();
            var newEnemies = new Bag<Enemy>();
            var newTags = new Bag<byte>();

            int aliveCount = 0;
            for (int entityId = 0; entityId < EntityCount; ++entityId)
            {
                var isAlive = Tags[entityId].IsBitSet((int)Tag.IsAlive);
                if (isAlive)
                {
                    aliveCount++;
                    newPositions.Add(Positions[entityId]);
                    newVelocities.Add(Velocities[entityId]);
                    newVolumes.Add(Volumes[entityId]);
                    newTextures.Add(Textures[entityId]);
                    newSizes.Add(Sizes[entityId]);
                    newRotations.Add(Rotations[entityId]);
                    newTimesSinceLastShot.Add(TimesSinceLastShot[entityId]);
                    newTimesSinceLastEnemySpawned.Add(TimesSinceLastEnemySpawned[entityId]);
                    newAnimationData.Add(AnimationData[entityId]);
                    newPlayers.Add(Players[entityId]);
                    newEnemies.Add(Enemies[entityId]);
                    newTags.Add(Tags[entityId]);
                }
            }

            Positions = newPositions;
            Velocities = newVelocities;
            Volumes = newVolumes;
            Textures = newTextures;
            Sizes = newSizes;
            Rotations = newRotations;
            TimesSinceLastShot = newTimesSinceLastShot;
            TimesSinceLastEnemySpawned = newTimesSinceLastEnemySpawned;
            AnimationData = newAnimationData;
            Players = newPlayers;
            Enemies = newEnemies;
            Tags = newTags;

            EntityCount = aliveCount;
            AliveEntityCount = aliveCount;
        }
    }
}