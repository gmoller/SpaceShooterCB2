using System;
using System.Collections;
using System.Collections.Generic;
using GameEngineCore.Interfaces;

namespace GameEngineCore
{
    /// <summary>
    /// Set of components for an entity.
    /// </summary>
    public class ComponentsSet : IEnumerable<IComponent>
    {
        private readonly List<IComponent> _components;

        public int EntityId { get; }
        public float LifeTime { get; set; }
        public bool IsDeleted { get; set; }

        public ComponentsSet(float lifeTime = float.PositiveInfinity)
        {
            _components = new List<IComponent>();
            EntityId = Registrar.Instance.AddEntity(this);
            LifeTime = lifeTime;
        }

        public void AddComponent(Type componentType, IComponent component)
        {
            component.EntityId = EntityId;
            component.ComponentType = componentType;
            _components.Add(component);
        }

        public IComponent this[Type componentType]
        {
            get
            {
                foreach (IComponent item in _components)
                {
                    if (item.ComponentType == componentType)
                    {
                        return item;
                    }
                }

                throw new Exception($"Component [{componentType}] not found in Components. Could not get the component.");
            }
        }

        public bool HasComponent(Type componentType)
        {
            foreach (IComponent item in _components)
            {
                if (item.ComponentType == componentType)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<IComponent> GetEnumerator()
        {
            foreach (IComponent item in _components)
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