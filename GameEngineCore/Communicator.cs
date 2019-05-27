using System;

namespace GameEngineCore
{
    public sealed class Communicator
    {
        private static readonly Lazy<Communicator> Lazy = new Lazy<Communicator>(() => new Communicator());

        public static Communicator Instance => Lazy.Value;

        private Communicator()
        {
        }

        public void Send(int entityId, Type componentType, string attributeName, object payload)
        {
            ComponentsSet components = Registrar.Instance.GetEntity(entityId);

            components[componentType].Receive(attributeName, payload);
        }
    }
}