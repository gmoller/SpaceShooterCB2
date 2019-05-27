using GameEngineCore.Interfaces;

namespace GameEngineCore.AbstractClasses
{
    public abstract class UpdateComponent : IComponent
    {
        public int EntityId { get; set; }
        public ComponentType ComponentType { get; set; }
        public abstract void Update(float deltaTime);
        public abstract void Receive(AttributeType attributeId, object payload);
    }
}