﻿using GameEngineCore;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Systems
{
    public class PlayerCollisionDetectionSystem : System
    {
        public PlayerCollisionDetectionSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var volume = GameState.GameData.Volumes[entityId];
            var player = GameState.GameData.Players[entityId];
             
            // selection
            if (player == null || player.Value.Status == PlayerStatus.Destroyed || volume == null) return;

            // process data
            var p = player.Value;
            for (int i = 0; i < GameState.EntityCount - 1; ++i)
            {
                if (entityId == i) continue; // do not check with self
                var isAliveTag = GameState.GameData.Tags[i].IsBitSet((int)Tag.IsAlive);
                if (!isAliveTag) continue;

                var volume2 = GameState.GameData.Volumes[i];
                if (volume2 == null) continue;

                // collision with enemy:
                var enemy = GameState.GameData.Enemies[i];
                var v = volume.Value;
                var v2 = volume2.Value;
                if (enemy != null && v.Intersects(v2))
                {
                    p.Status = PlayerStatus.Destroyed;
                    break;
                }

                // collision with projectile:
                var isProjectileTag = GameState.GameData.Tags[i].IsBitSet((int)Tag.IsProjectile);
                if (isProjectileTag && v.Intersects(v2))
                {
                    p.Status = PlayerStatus.Destroyed;
                    break;
                }
            }

            // update data
            if (p.Status == PlayerStatus.Destroyed)
            {
                p.DeathCooldownTime = 3000.0f; // 3 seconds
                GameState.GameData.Players[entityId] = p;
                GameState.GameData.Velocities[entityId] = Vector2.Zero;
            }
        }
    }
}