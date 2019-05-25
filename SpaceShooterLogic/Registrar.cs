using System;
using System.Collections.Generic;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic
{
    public sealed class Registrar
    {
        private static readonly Lazy<Registrar> Lazy = new Lazy<Registrar>(() => new Registrar());

        public static Registrar Instance => Lazy.Value;

        private readonly Dictionary<int, ComponentsSet> _entityComponents;

        private Registrar()
        {
            _entityComponents = new Dictionary<int, ComponentsSet>();
        }

        public void Add(int entityId, ComponentsSet componentsSet)
        {
            _entityComponents[entityId] = componentsSet;
        }

        public void Remove(int entityId)
        {
            // TODO: flag as deleted
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