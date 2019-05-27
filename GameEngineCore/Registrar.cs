using System;

namespace GameEngineCore
{
    public sealed class Registrar
    {
        private static readonly Lazy<Registrar> Lazy = new Lazy<Registrar>(() => new Registrar());

        public static Registrar Instance => Lazy.Value;

        private readonly Entities _entities;

        private Registrar()
        {
            _entities = new Entities();
        }

        public void Clear()
        {
            _entities.Clear();
        }

        public int AddEntity(ComponentsSet componentsSet)
        {
            return _entities.AddEntity(componentsSet);
        }

        public void RemoveEntity(int entityId)
        {
            _entities.RemoveEntity(entityId);
        }

        public ComponentsSet GetEntity(int entityId)
        {
            return _entities.GetEntity(entityId);
        }

        public Entities GetAllEntities()
        {
            return _entities;
        }

        public Entities FilterEntities(params Type[] componentTypes)
        {
            return _entities.FilterEntities(componentTypes);
        }
    }
}