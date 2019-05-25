using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Components
{
    public interface IComponent
    {
        void Send(Player player);
        void Receive(AttributeType attributeId, object payload);
        void Receive(AttributeType attributeId, Vector2 payload);
        void Receive(AttributeType attributeId, Rectangle payload);
    }
}