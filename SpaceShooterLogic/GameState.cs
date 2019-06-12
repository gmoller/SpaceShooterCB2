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
        public GameData2 GameData2 { get; }

        public SpriteBatchList SpriteBatchList { get; }
        public SoundEffectList SoundEffectList { get; }

        public BenchmarkMetrics Metrics { get; }

        public GameState()
        {
            Metrics = new BenchmarkMetrics();
            SpriteBatchList = new SpriteBatchList();
            SoundEffectList = new SoundEffectList();

            GameData = new GameData();
            GameData2 = new GameData2();
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

            GameData.Clear();
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