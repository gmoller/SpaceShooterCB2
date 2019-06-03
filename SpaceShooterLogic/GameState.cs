using AnimationLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic
{
    public class GameState
    {
        public Bag<byte> Tags { get; private set; }
        public Bag<Vector2?> Velocities { get; private set; }
        public Bag<Vector2?> Positions { get; private set; }
        public Bag<Rectangle?> Volumes { get; private set; }
        public Bag<Texture2D> Textures { get; private set; }
        public Bag<Vector2?> Sizes { get; private set; }
        public Bag<int?> CurrentFrames { get; private set; }
        public Bag<float?> Rotations { get; private set; }
        public Bag<float?> TimesSinceLastShot { get; private set; }
        public Bag<AnimationSpec> AnimationSpecs { get; private set; }
        public Bag<float?> TimesSinceLastAnimationChange { get; private set; }

        public GameState()
        {
            ClearState();
        }

        public void ClearState()
        {
            Tags = new Bag<byte>();
            Velocities = new Bag<Vector2?>();
            Positions = new Bag<Vector2?>();
            Volumes = new Bag<Rectangle?>();
            Textures = new Bag<Texture2D>();
            Sizes = new Bag<Vector2?>();
            CurrentFrames = new Bag<int?>();
            Rotations = new Bag<float?>();
            TimesSinceLastShot = new Bag<float?>();
            AnimationSpecs = new Bag<AnimationSpec>();
            TimesSinceLastAnimationChange = new Bag<float?>();
        }
    }
}