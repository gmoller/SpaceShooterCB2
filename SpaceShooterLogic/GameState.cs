using AnimationLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic
{
    public class GameState
    {
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

        public Bag<byte> Tags { get; private set; }

        public GameState()
        {
            ClearState();
        }

        public void ClearState()
        {
            Positions = new Bag<Vector2>();
            Velocities = new Bag<Vector2>();
            Volumes = new Bag<Rectangle>();
            Textures = new Bag<Texture2D>();
            Sizes = new Bag<Vector2>();

            Rotations = new Bag<float>();

            TimesSinceLastShot = new Bag<float>();
            TimesSinceLastEnemySpawned = new Bag<float>();

            AnimationData = new Bag<AnimationData>();

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
}