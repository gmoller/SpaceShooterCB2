using AnimationLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterLogic.Components;
using SpaceShooterUtilities;

namespace SpaceShooterLogic
{
    public class Player
    {
        private readonly IInputComponent _input;
        private readonly IPhysicsComponent _physics;
        private readonly IGraphicsComponent _graphics;
        private readonly ILaserComponent _laser;

        public Vector2 Scale { get; }
        public Vector2 Position { get; set; } // center position of entity

        public bool IsDead { get; private set; }
        public bool IsAlive => !IsDead;
        public PhysicsBody PhysicsBody => _physics.Body;

        internal Player(Texture2D texture, Vector2 position, IInputComponent inputComponent)
        {
            Scale = new Vector2(2.5f, 2.5f);
            Position = position;

            _input = inputComponent;
            _physics = new PlayerPhysicsComponent(this);
            _graphics = new PlayerGraphicsComponent(texture);
            _laser = new PlayerLaserComponent();
        }

        public void Update(GameTime gameTime)
        {
            if (IsDead) return;

            Send(ComponentType.Input, Position);
            _input.Update(this, gameTime);
            _graphics.Update(gameTime);
            _physics.Update(this, gameTime);
            _laser.Update(this, gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsDead) return;

            _graphics.Draw(this, spriteBatch);
        }

        public void KillPlayer()
        {
            int i = RandomGenerator.Instance.GetRandomInt(0, 1);
            SoundEffect sndExplode = AssetsManager.Instance.GetSound($"sndExplode{i}");
            sndExplode.Play();

            Texture2D texture = AssetsManager.Instance.GetTexture("Fireball02");
            AnimationSpec animationSpec = AssetsManager.Instance.GetAnimations(texture.Name);
            var explosionPosition = new Vector2(Position.X, Position.Y);
            var playerSize = _physics.Body.Size;
            var explosion = new Explosion(texture, animationSpec, explosionPosition, playerSize);
            GameEntitiesManager.Instance.Explosions.Add(explosion);
            IsDead = true;
        }

        internal void Send(ComponentType componentId, object payload)
        {
            switch (componentId)
            {
                case ComponentType.Graphics:
                    _graphics.Receive(payload);
                    break;
                case ComponentType.Input:
                    _input.Receive(payload);
                    break;
                case ComponentType.Laser:
                    _laser.Receive(payload);
                    break;
                case ComponentType.Physics:
                    _physics.Receive(payload);
                    break;
            }
        }
    }
}