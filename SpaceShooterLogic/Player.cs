using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic
{
    public class Player
    {
        private readonly IInputComponent _input;
        private readonly IPhysicsComponent _physics;
        private readonly ILaserComponent _laser;
        private readonly IGraphicsComponent _graphics;

        public Vector2 Position { get; set; } // center position of entity

        public bool IsDead { get; set; }

        internal Player(string textureName, Vector2 position, IInputComponent inputComponent)
        {
            Position = position;

            _input = inputComponent;
            _physics = new PlayerPhysicsComponent(this, position);
            _laser = new PlayerLaserComponent();
            _graphics = new PlayerGraphicsComponent(textureName);
        }

        public void Update(GameTime gameTime)
        {
            if (IsDead) return;

            _input.Update(this, gameTime);
            _physics.Update(this, gameTime);
            _laser.Update(this, gameTime);
            _graphics.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsDead) return;

            _graphics.Draw(spriteBatch);
        }

        internal void Send(ComponentType componentId, AttributeType attributeId, object payload)
        {
            switch (componentId)
            {
                case ComponentType.Graphics:
                    _graphics?.Receive(attributeId, payload);
                    break;
                case ComponentType.Input:
                    _input.Receive(attributeId, payload);
                    break;
                case ComponentType.Laser:
                    _laser.Receive(attributeId, payload);
                    break;
                case ComponentType.Physics:
                    _physics.Receive(attributeId, payload);
                    break;
            }
        }

        internal void Send(ComponentType componentId, AttributeType attributeId, Vector2 payload)
        {
            switch (componentId)
            {
                case ComponentType.Graphics:
                    _graphics?.Receive(attributeId, payload);
                    break;
                case ComponentType.Input:
                    _input.Receive(attributeId, payload);
                    break;
                case ComponentType.Laser:
                    _laser.Receive(attributeId, payload);
                    break;
                case ComponentType.Physics:
                    _physics.Receive(attributeId, payload);
                    break;
            }
        }

        internal void Send(ComponentType componentId, AttributeType attributeId, Rectangle payload)
        {
            switch (componentId)
            {
                case ComponentType.Graphics:
                    _graphics?.Receive(attributeId, payload);
                    break;
                case ComponentType.Input:
                    _input.Receive(attributeId, payload);
                    break;
                case ComponentType.Laser:
                    _laser.Receive(attributeId, payload);
                    break;
                case ComponentType.Physics:
                    _physics.Receive(attributeId, payload);
                    break;
            }
        }
    }
}