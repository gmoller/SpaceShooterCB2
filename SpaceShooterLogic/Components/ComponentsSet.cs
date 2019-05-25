using System.Collections;
using System.Collections.Generic;

namespace SpaceShooterLogic.Components
{
    /// <summary>
    /// Set of components for an entity.
    /// </summary>
    public class ComponentsSet : IEnumerable<Component>
    {
        private readonly Dictionary<ComponentType, Component> _components;

        public ComponentsSet(int entityId)
        {
            _components = new Dictionary<ComponentType, Component>();

            Registrar.Instance.Add(entityId, this);
        }

        public void Add(ComponentType componentType, Component component)
        {
            _components.Add(componentType, component);
        }

        public Component this[ComponentType index] => _components[index];

        public IEnumerator<Component> GetEnumerator()
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