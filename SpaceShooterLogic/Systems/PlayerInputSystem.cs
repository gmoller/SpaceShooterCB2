﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Systems
{
    public class PlayerInputSystem : System
    {
        private readonly float _movementSpeed; // pixels per millisecond

        public PlayerInputSystem(string name, GameState gameState) : base(name, gameState)
        {
            _movementSpeed = 0.24f;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var tag = GameState.Tags[entityId].IsBitSet((int)Tag.PlayerInput);

            // selection
            if (!tag) return;

            // process data
            var direction = Vector2.Zero;
            var shoot = false;
            if (KeyboardHandler.IsKeyDown(Keys.Up))
            {
                direction = new Vector2(direction.X, -1.0f);
            }
            if (KeyboardHandler.IsKeyDown(Keys.Down))
            {
                direction = new Vector2(direction.X, 1.0f);
            }
            if (KeyboardHandler.IsKeyDown(Keys.Left))
            {
                direction = new Vector2(-1.0f, direction.Y);
            }
            if (KeyboardHandler.IsKeyDown(Keys.Right))
            {
                direction = new Vector2(1.0f, direction.Y);
            }
            if (KeyboardHandler.IsKeyDown(Keys.RightControl))
            {
                shoot = true;
            }

            // update data
            GameState.Velocities[entityId] = direction * _movementSpeed;
            if (shoot)
            {
                GameState.Tags[entityId] = GameState.Tags[entityId].SetBit((int)Tag.PlayerShoots);
            }
            else
            {
                GameState.Tags[entityId] = GameState.Tags[entityId].UnsetBit((int)Tag.PlayerShoots);
            }
            
        }
    }
}