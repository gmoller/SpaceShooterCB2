namespace GameEngineCore.Interfaces
{
    public interface IComponent
    {
        int EntityId { get; set; }
        ComponentType ComponentType { get; set; }
        void Receive(AttributeType attributeId, object payload);
    }
}