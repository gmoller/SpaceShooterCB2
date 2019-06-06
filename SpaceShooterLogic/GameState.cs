﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationLibrary;
using GameEngineCore;
using SpaceShooterLogic.Screens;
using Microsoft.Xna.Framework.Audio;

namespace SpaceShooterLogic
{
    public class GameState
    {
        public Hud Hud { get; }

        public bool PlayerIsDead { get; set; }
        public int Score { get; set; }
        public int Lives { get; private set; }

        public int EntityCount { get; set; }

        #region Component Type Arrays

        // standard entity
        public Bag<Vector2> Positions { get; private set; }
        public Bag<Vector2> Velocities { get; private set; }
        public Bag<Rectangle> Volumes { get; private set; }
        public Bag<Texture2D> Textures { get; private set; }
        public Bag<Vector2> Sizes { get; private set; }

        public Bag<float> Rotations { get; private set; }

        public Bag<float> TimesSinceLastShot { get; private set; }
        public Bag<float> TimesSinceLastEnemySpawned { get; private set; }

        // animations
        public Bag<AnimationData> AnimationData { get; private set; }

        public Bag<byte> ScoreValues { get; private set; }

        public Bag<byte> Tags { get; private set; }

        #endregion

        public SpriteBatchList SpriteBatchList { get; }
        public SoundEffectList SoundEffectList { get; }

        public BenchmarkMetrics Metrics { get; }

        public GameState()
        {
            Metrics = new BenchmarkMetrics();
            SpriteBatchList = new SpriteBatchList();
            SoundEffectList = new SoundEffectList();
            ClearState();
            Score = 0;
            Lives = 3;

            Hud = new Hud(this);
        }

        public void AddToSpriteBatchList(Texture2D texture, Vector2 position, Rectangle frame, float rotation, Vector2 origin, Vector2 scale, Rectangle volume)
        {
            SpriteBatchList.Add(texture, position, frame, rotation, origin, scale, volume);
        }

        public void AddToSoundEffectList(SoundEffect sound)
        {
            SoundEffectList.Add(sound);
        }

        public void Restart()
        {
            ClearState();
            Lives--;
            PlayerIsDead = false;
        }

        public void ClearState()
        {
            EntityCount = 0;

            Positions = new Bag<Vector2>();
            Velocities = new Bag<Vector2>();
            Volumes = new Bag<Rectangle>();
            Textures = new Bag<Texture2D>();
            Sizes = new Bag<Vector2>();
            Rotations = new Bag<float>();
            TimesSinceLastShot = new Bag<float>();
            TimesSinceLastEnemySpawned = new Bag<float>();
            AnimationData = new Bag<AnimationData>();
            ScoreValues = new Bag<byte>();
            Tags = new Bag<byte>();
        }
    }

    public struct AnimationData
    {
        public AnimationSpec AnimationSpec { get; }
        public int CurrentFrame { get; set; }
        public float TimeSinceLastAnimationChange { get; set; }

        public AnimationData(AnimationSpec animationSpec, int currentFrame, float timeSinceLastAnimationChange)
        {
            AnimationSpec = animationSpec;
            CurrentFrame = currentFrame;
            TimeSinceLastAnimationChange = timeSinceLastAnimationChange;
        }
    }

    public enum Tag
    {
        IsAlive = 0,
        PlayerInput = 1,
        PlayerShoots = 2,
        ClampToViewport = 3,
        DestroyIfOutsideViewport = 4,
        CollisionDetected = 5,
        EnemyIsChaser = 6,
        EnemyIsShooter = 7
    }
}