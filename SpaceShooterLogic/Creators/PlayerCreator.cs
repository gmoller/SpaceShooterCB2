﻿using GameEngineCore;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Creators
{
    public static class PlayerCreator
    {
        public static Entity Create(UpdateComponent inputComponent)
        {
            Vector2 size = new Vector2(16.0f, 16.0f) * 2.5f;

            var components = new ComponentsSet();
            components.AddComponent(typeof(PlayerInputComponent), inputComponent);
            components.AddComponent(typeof(PlayerPhysicsComponent), new PlayerPhysicsComponent(DeviceManager.Instance.ScreenDimensions * 0.5f, size));
            components.AddComponent(typeof(LaserComponent), new LaserComponent(new Vector2(0.0f, -1.0f), new Vector2(0.0f, -size.Y), 0.600f, 200));
            components.AddComponent(typeof(SpriteComponent), new SpriteComponent("sprPlayer"));
            components.AddComponent(typeof(GraphicsComponent), new GraphicsComponent("sprPlayer", Vector2.Zero, size));
            //components.AddComponent(typeof(VolumeGraphicsComponent), new VolumeGraphicsComponent(new Rectangle()));

            var player = new Entity(components);

            return player;
        }
    }
}