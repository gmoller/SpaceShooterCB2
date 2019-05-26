using System;
using System.Collections.Generic;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic
{
    public sealed class Registrar
    {
        private static readonly Lazy<Registrar> Lazy = new Lazy<Registrar>(() => new Registrar());

        public static Registrar Instance => Lazy.Value;

        private int _nextEntityId;
        private readonly Dictionary<int, ComponentsSet> _entityComponents;

        private Registrar()
        {
            _nextEntityId = 1;
            _entityComponents = new Dictionary<int, ComponentsSet>();
        }

        public int AddComponentSet(ComponentsSet componentsSet)
        {
            int entityId = _nextEntityId++;
            _entityComponents[entityId] = componentsSet;

            return entityId;
        }

        public void Remove(int entityId)
        {
            // TODO: flag as deleted
            ComponentsSet components = _entityComponents[entityId];
            components.IsDeleted = true;
            //_entityComponents.Remove(entityId);
        }

        public ComponentsSet GetComponentsForEntity(int entityId)
        {
            try
            {
                return _entityComponents[entityId];
            }
            catch (Exception ex)
            {
                throw new Exception($"EntityId [{entityId}] not found in EntityComponents. Could not get it's components.", ex);
            }
        }
    }
}