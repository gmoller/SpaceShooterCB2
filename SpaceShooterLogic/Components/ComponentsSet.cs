using System;
using System.Collections;
using System.Collections.Generic;

namespace SpaceShooterLogic.Components
{
    /// <summary>
    /// Set of components for an entity.
    /// </summary>
    public class ComponentsSet : IEnumerable<IComponent>
    {
        private readonly Dictionary<ComponentType, IComponent> _components;

        public int EntityId { get; }
        public bool IsDeleted { get; set; }

        public ComponentsSet()
        {
            _components = new Dictionary<ComponentType, IComponent>();
            EntityId = Registrar.Instance.AddComponentSet(this);
        }

        public void AddUpdateComponent(ComponentType componentType, UpdateComponent component)
        {
            component.EntityId = EntityId;
            _components.Add(componentType, component);
        }

        public void AddDrawComponent(ComponentType componentType, DrawComponent component)
        {
            component.EntityId = EntityId;
            _components.Add(componentType, component);
        }

        public IComponent this[ComponentType index]
        {
            get
            {
                try
                {
                    return _components[index];
                }
                catch (Exception ex)
                {
                    throw new Exception($"Component [{index}] not found in Components. Could not get the component.", ex);
                }
            }
        }

        public IEnumerator<IComponent> GetEnumerator()
        {
            foreach (var item in _components.Values)
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