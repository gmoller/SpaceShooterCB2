using AnimationLibrary;
using GameEngineCore;
using GameEngineCore.AbstractClasses;
using Microsoft.Xna.Framework;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Components
{
    public class SpriteComponent : UpdateComponent
    {
        private readonly AnimatedSprite _sprite;

        internal SpriteComponent(string textureName)
        {
            AnimationSpec animationSpec = AssetsManager.Instance.GetAnimations(textureName);
            _sprite = new AnimatedSprite(animationSpec);
        }

        public override void Update(float deltaTime)
        {
            _sprite.Update(deltaTime);

            Send(_sprite.GetCurrentFrame());
        }

        #region Send & Recieve
        public void Send(Rectangle frame)
        {
            Communicator.Instance.Send(EntityId, ComponentType.Graphics, AttributeType.GraphicsFrame, frame);
        }

        public override void Receive(AttributeType attributeId, object payload)
        {
        }
        #endregion
    }
}