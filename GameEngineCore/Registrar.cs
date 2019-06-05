//using System;

//namespace GameEngineCore
//{
//    public sealed class Registrar
//    {
//        private static readonly Lazy<Registrar> Lazy = new Lazy<Registrar>(() => new Registrar());

//        public static Registrar Instance => Lazy.Value;

//        private readonly Entities _entities;

//        public int EntityCount { get; set; }

//        private Registrar()
//        {
//            EntityCount = 0;

//            _entities = new Entities();
//        }

//        #region Old
//        public void Clear()
//        {
//            _entities.Clear();
//            EntityCount = 0;
//        }

//        public int AddEntity(ComponentsSet componentsSet)
//        {
//            return _entities.AddEntity(componentsSet);
//        }

//        public void RemoveEntity(int entityId)
//        {
//            _entities.RemoveEntity(entityId);
//        }

//        public ComponentsSet GetEntity(int entityId)
//        {
//            return _entities.GetEntity(entityId);
//        }

//        public Entities GetAllEntities()
//        {
//            return _entities;
//        }

//        public Entities FilterEntities(Operator op, params Type[] componentTypes)
//        {
//            return _entities.FilterEntities(op, componentTypes);
//        }
//        #endregion
//    }
//}