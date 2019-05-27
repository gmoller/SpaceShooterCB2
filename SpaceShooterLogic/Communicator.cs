using System;
using GameEngineCore;

namespace SpaceShooterLogic
{
    public sealed class Communicator
    {
        private static readonly Lazy<Communicator> Lazy = new Lazy<Communicator>(() => new Communicator());

        public static Communicator Instance => Lazy.Value;

        private Communicator()
        {
        }

        public void Send(int entityId, ComponentType componentType, AttributeType attributeId, object payload)
        {
            ComponentsSet components = Registrar.Instance.GetEntity(entityId);

            components[componentType].Receive(attributeId, payload);
        }
    }
}