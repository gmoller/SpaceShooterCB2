﻿using Microsoft.Xna.Framework;
using GameEngineCore;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class ProjectileCreator
    {
        public static void Create(string textureName, Vector2 projectilePosition, Vector2 projectileVelocity, GameState state)
        {
            Vector2 size = new Vector2(1.0f, 8.0f);

            int entityId = state.EntityCount;

            state.Transforms[entityId] = new Transform(projectilePosition, 0.0f, Vector2.One, new Vector2(1.0f, 8.0f));
            state.Velocities[entityId] = projectileVelocity;
            state.Volumes[entityId] = new Rectangle((int)(projectilePosition.X - 0.5f), (int)(projectilePosition.Y - 4.0f), (int)size.X, (int)size.Y);
            state.Textures[entityId] = AssetsManager.Instance.GetTexture(textureName);
            state.Tags[entityId] = state.Tags[entityId].SetBits((int)Tag.IsAlive, (int)Tag.IsProjectile);

            state.EntityCount++;

            var sound = AssetsManager.Instance.GetSound("sndLaser");
            state.AddToSoundEffectList(sound);
            state.AliveEntityCount++;
        }
    }
}