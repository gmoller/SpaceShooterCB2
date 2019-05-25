using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Components
{
    public abstract class Component
    {
        protected int EntityId { get; }
        public abstract void Update(GameTime gameTime);
        public abstract void Receive(AttributeType attributeId, object payload);

        protected Component(int entityId)
        {
            EntityId = entityId;
        }
    }
}