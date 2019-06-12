using System.Collections.Generic;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Data
{
    public class GameData2
    {
        public int EntityCount { get; private set; }

        public List<IGameComponent> Transform { get; private set; }
        public List<IGameComponent> Velocity { get; private set; }
        public List<IGameComponent> Volume { get; private set; }
        public List<IGameComponent> Texture { get; private set; }
        public List<IGameComponent> AnimationData { get; private set; }
        public List<IGameComponent> Player { get; private set; }
        public List<IGameComponent> Enemy { get; private set; }
        public List<IGameComponent> Weapon { get; private set; }
        public List<IGameComponent> EnemySpawner { get; private set; }

        public GameData2()
        {
            Clear();
        }

        public int GetNextEntityId()
        {
            return EntityCount++;
        }

        public void Clear()
        {
            Transform = new List<IGameComponent>();
            Velocity = new List<IGameComponent>();
            Volume = new List<IGameComponent>();
            Texture = new List<IGameComponent>();
            AnimationData = new List<IGameComponent>();
            Player = new List<IGameComponent>();
            Enemy = new List<IGameComponent>();
            Weapon = new List<IGameComponent>();
            EnemySpawner = new List<IGameComponent>();

            EntityCount = 0;
        }
    }
}