﻿using GameEngineCore;
using SpaceShooterLogic.Creators;

namespace SpaceShooterLogic.Systems
{
    public class CollisionResolutionSystem : System
    {
        private readonly float _explosionSizeScaleFactor;

        public CollisionResolutionSystem(string name, GameState gameState) : base(name, gameState)
        {
            _explosionSizeScaleFactor = 5.0f;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var collisionDetectedTag = GameState.Tags[entityId].IsBitSet((int)Tag.CollisionDetected);

            // selection
            if (!collisionDetectedTag) return;

            // gather data for processing
            var position = GameState.Positions[entityId];
            var size = GameState.Sizes[entityId];
            var enemy = GameState.Enemies[entityId];

            // process data
            ExplosionCreator.Create("Explosion10", position, size * _explosionSizeScaleFactor, GameState);

            var p = GameState.FindPlayer();
            p.player.Score += enemy.Score;
            GameState.Players[p.index] = p.player;

            // update data
            GameState.Tags[entityId] = GameState.Tags[entityId].UnsetBits((int)Tag.IsAlive, (int)Tag.CollisionDetected);
        }
    }
}