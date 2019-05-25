namespace SpaceShooterLogic.Components
{
    public interface IComponent
    {
        void Send(Player player);
        void Receive(AttributeType attributeId, object payload);
    }
}