using System;

namespace GameEngineCore.Interfaces
{
    public interface IComponent
    {
        int EntityId { get; set; }
        Type ComponentType { get; set; }
        void Receive(string attributeName, object payload);
    }
}