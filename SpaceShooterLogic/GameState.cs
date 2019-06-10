using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using GameEngineCore;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic
{
    public class GameState
    {
        public bool GameOver { get; set; }

        public int EntityCount { get; set; }
        public int AliveEntityCount { get; set; }
        public float AlivePercentage => AliveEntityCount / (float)EntityCount * 100;

        #region Component Type Arrays

        public Bag<Transform?> Transforms { get; private set; }
        public Bag<Vector2?> Velocities { get; private set; }
        public Bag<Rectangle?> Volumes { get; private set; }
        public Bag<Texture2D> Textures { get; private set; }
        public Bag<AnimationData?> AnimationData { get; private set; }

        public Bag<Player?> Players { get; private set; }
        public Bag<Enemy?> Enemies { get; private set; }
        public Bag<Weapon?> Weapons { get; private set; }

        public Bag<EnemySpawner?> EnemySpawner { get; private set; }

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

            Transforms = new Bag<Transform?>();
            Velocities = new Bag<Vector2?>();
            Volumes = new Bag<Rectangle?>();
            Textures = new Bag<Texture2D>();
            EnemySpawner = new Bag<EnemySpawner?>();
            AnimationData = new Bag<AnimationData?>();
            Players = new Bag<Player?>();
            Enemies = new Bag<Enemy?>();
            Weapons = new Bag<Weapon?>();
            Tags = new Bag<byte>();
        }

        public (Player player, int index) FindPlayer()
        {
            for (int i = 0; i < EntityCount - 1; ++i)
            {
                var player = Players[i];
                if (player != null)
                {
                    return (player.Value, i);
                }
            }

            throw new Exception("Player not found!");
        }

        public void CompactEntities()
        {
            var newTransforms = new Bag<Transform?>();
            var newVelocities = new Bag<Vector2?>();
            var newVolumes = new Bag<Rectangle?>();
            var newTextures = new Bag<Texture2D>();
            var newEnemySpawner = new Bag<EnemySpawner?>();
            var newAnimationData = new Bag<AnimationData?>();
            var newPlayers = new Bag<Player?>();
            var newEnemies = new Bag<Enemy?>();
            var newWeapons = new Bag<Weapon?>();
            var newTags = new Bag<byte>();

            int aliveCount = 0;
            for (int entityId = 0; entityId < EntityCount; ++entityId)
            {
                var isAlive = Tags[entityId].IsBitSet((int)Tag.IsAlive);
                if (isAlive)
                {
                    aliveCount++;
                    newTransforms.Add(Transforms[entityId]);
                    newVelocities.Add(Velocities[entityId]);
                    newVolumes.Add(Volumes[entityId]);
                    newTextures.Add(Textures[entityId]);
                    newEnemySpawner.Add(EnemySpawner[entityId]);
                    newAnimationData.Add(AnimationData[entityId]);
                    newPlayers.Add(Players[entityId]);
                    newEnemies.Add(Enemies[entityId]);
                    newWeapons.Add(Weapons[entityId]);
                    newTags.Add(Tags[entityId]);
                }
            }

            Transforms = newTransforms;
            Velocities = newVelocities;
            Volumes = newVolumes;
            Textures = newTextures;
            EnemySpawner = newEnemySpawner;
            AnimationData = newAnimationData;
            Players = newPlayers;
            Enemies = newEnemies;
            Weapons = newWeapons;
            Tags = newTags;

            EntityCount = aliveCount;
            AliveEntityCount = aliveCount;
        }
    }
}