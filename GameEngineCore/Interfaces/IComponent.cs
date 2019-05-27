using System;

namespace GameEngineCore.Interfaces
{
    public interface IComponent
    {
        int EntityId { get; set; }
        Type ComponentType { get; set; }
        void Receive(AttributeType attributeId, object payload);
    }
}