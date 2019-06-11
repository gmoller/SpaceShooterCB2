using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using GameEngineCore;
using SpaceShooterLogic.Components;
using SpaceShooterLogic.Data;

namespace SpaceShooterLogic
{
    public class GameState
    {
        public bool GameOver { get; set; }

        public int EntityCount { get; set; }
        public int AliveEntityCount { get; set; }
        public float AlivePercentage => AliveEntityCount / (float)EntityCount * 100;

        public GameData GameData { get; }

        #region Component Type Arrays

        //public Bag<Transform?> Transforms { get; private set; }
        //public Bag<Vector2?> Velocities { get; private set; }
        //public Bag<Rectangle?> Volumes { get; private set; }
        //public Bag<Texture2D> Textures { get; private set; }
        //public Bag<AnimationData?> AnimationData { get; private set; }

        //public Bag<Player?> Players { get; private set; }
        //public Bag<Enemy?> Enemies { get; private set; }
        //public Bag<Weapon?> Weapons { get; private set; }

        //public Bag<EnemySpawner?> EnemySpawner { get; private set; }

        //public Bag<byte> Tags { get; private set; }

        #endregion

        public SpriteBatchList SpriteBatchList { get; }
        public SoundEffectList SoundEffectList { get; }

        public BenchmarkMetrics Metrics { get; }

        public GameState()
        {
            Metrics = new BenchmarkMetrics();
            SpriteBatchList = new SpriteBatchList();
            SoundEffectList = new SoundEffectList();

            GameData = new GameData();
            GameData.Clear();
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

            //Transforms = new Bag<Transform?>();
            //Velocities = new Bag<Vector2?>();
            //Volumes = new Bag<Rectangle?>();
            //Textures = new Bag<Texture2D>();
            //EnemySpawner = new Bag<EnemySpawner?>();
            //AnimationData = new Bag<AnimationData?>();
            //Players = new Bag<Player?>();
            //Enemies = new Bag<Enemy?>();
            //Weapons = new Bag<Weapon?>();
            //Tags = new Bag<byte>();
        }

        public (Player player, int index) FindPlayer()
        {
            for (int i = 0; i < EntityCount - 1; ++i)
            {
                var player = GameData.Players[i];
                if (player != null)
                {
                    return (player.Value, i);
                }
            }

            throw new Exception("Player not found!");
        }
    }
}