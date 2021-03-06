﻿using GameEngineCore;
using SpaceShooterLogic.Creators;

namespace SpaceShooterLogic.Systems
{
    public class CollisionResolutionSystem : System
    {
        private readonly float _explosionSizeScaleFactor;

        public CollisionResolutionSystem(string name, GameState gameState) : base(name, gameState)
        {
            _explosionSizeScaleFactor = 10.0f;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var collisionDetectedTag = GameState.GameData.Tags[entityId].IsBitSet((int)Tag.CollisionDetected);

            // selection
            if (!collisionDetectedTag) return;

            // gather data for processing
            var transform = GameState.GameData.Transforms[entityId];
            var enemy = GameState.GameData.Enemies[entityId];

            // process data
            var t = transform.Value;
            ExplosionCreator.Create("Explosion10", t.Position, _explosionSizeScaleFactor, t.Size * t.Scale, GameState);

            var p = GameState.FindPlayer();
            p.player.Score += enemy.Value.Score;
            GameState.GameData.Players[p.index] = p.player;

            // update data
            GameState.GameData.Tags[entityId] = GameState.GameData.Tags[entityId].UnsetBits((int)Tag.IsAlive, (int)Tag.CollisionDetected);
        }
    }
}