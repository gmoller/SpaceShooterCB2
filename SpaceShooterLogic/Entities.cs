using System;
using System.Collections;
using System.Collections.Generic;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic
{
    public class Entities : IEnumerable<ComponentsSet>
    {
        private readonly List<ComponentsSet> _entityComponents;

        public Entities()
        {
            _entityComponents = new List<ComponentsSet>();
        }

        public void Clear()
        {
            _entityComponents.Clear();
        }

        public int AddEntity(ComponentsSet componentsSet)
        {
            _entityComponents.Add(componentsSet);

            return _entityComponents.Count - 1;
        }

        public void RemoveEntity(int entityId)
        {
            ComponentsSet components = _entityComponents[entityId];
            components.IsDeleted = true;
            //_entityComponents.Remove(entityId);
        }

        public ComponentsSet GetEntity(int entityId)
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

        public Entities FilterEntities(params ComponentType[] componentTypes)
        {
            var list = new Entities();
            foreach (ComponentsSet componentsSet in _entityComponents)
            {
                if (componentsSet.IsDeleted) continue;
                bool match = AllComponentsExistInComponentsSet(componentTypes, componentsSet);

                if (match)
                {
                    list.AddEntity(componentsSet);
                }
            }

            return list;
        }

        private bool AllComponentsExistInComponentsSet(ComponentType[] componentTypes, ComponentsSet componentsSet)
        {
            foreach (ComponentType componentType in componentTypes)
            {
                // does component exist in componentsSet?
                bool foundComponent = SingleComponentExistsInComponentsSet(componentType, componentsSet);

                if (!foundComponent)
                {
                    return false;
                }
            }

            return true;
        }

        private bool SingleComponentExistsInComponentsSet(ComponentType componentType, ComponentsSet componentsSet)
        {
            foreach (IComponent component in componentsSet)
            {
                if (component.ComponentType == componentType)
                {
                    return true;
                }
            }

            return false;
        }

        public ComponentsSet this[int entityId] => GetEntity(entityId);

        public IEnumerator<ComponentsSet> GetEnumerator()
        {
            foreach (ComponentsSet item in _entityComponents)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}