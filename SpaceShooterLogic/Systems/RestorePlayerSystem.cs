﻿using SpaceShooterLogic.Components;
using SpaceShooterLogic.Creators;

namespace SpaceShooterLogic.Systems
{
    public class RestorePlayerSystem : System
    {
        private readonly float _deathRotationSpeed;

        public RestorePlayerSystem(string name, GameState gameState) : base(name, gameState)
        {
            _deathRotationSpeed = 0.0075f;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var player = GameState.GameData.Players[entityId];

            // selection
            if (player == null || player.Value.Status != PlayerStatus.Destroyed) return;

            // process data
            var transform = GameState.GameData.Transforms[entityId].Value;
            transform.Rotation += _deathRotationSpeed * deltaTime;

            var p = player.Value;
            if (!p.DeathOnCooldown)
            {
                p.DeathCooldownTime = 0.0f;
                p.Lives--;
                transform.Rotation = 0.0f;
                p.Status = PlayerStatus.Alive;

                GameState.ClearState();

                PlayerCreator.Create(GameState, p.Score, p.Lives.GetValueOrDefault());
                EnemySpawnerCreator.Create(GameState);
            }

            p.DeathCooldownTime -= deltaTime;

            // update data
            GameState.GameData.Players[entityId] = p;
            GameState.GameData.Transforms[entityId] = transform;
        }
    }
}