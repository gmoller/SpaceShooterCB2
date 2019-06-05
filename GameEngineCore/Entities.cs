//using System;
//using System.Collections;
//using System.Collections.Generic;

//namespace GameEngineCore
//{
//    public class Entities : IEnumerable<ComponentsSet>
//    {
//        private readonly List<ComponentsSet> _entityComponents;

//        public Entities()
//        {
//            _entityComponents = new List<ComponentsSet>();
//        }

//        public int Count => _entityComponents.Count;

//        public void Clear()
//        {
//            _entityComponents.Clear();
//        }

//        public int AddEntity(ComponentsSet componentsSet)
//        {
//            _entityComponents.Add(componentsSet); // TODO: instead of adding to end of list, check if there is a deleted spot and overwrite if there is one

//            return _entityComponents.Count - 1;
//        }

//        public void RemoveEntity(int entityId)
//        {
//            ComponentsSet components = _entityComponents[entityId];
//            components.IsDeleted = true;
//            //_entityComponents.Remove(entityId);
//        }

//        public ComponentsSet GetEntity(int entityId)
//        {
//            try
//            {
//                return _entityComponents[entityId];
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"EntityId [{entityId}] not found in EntityComponents. Could not get it's components.", ex);
//            }
//        }

//        public Entities FilterEntities(Operator op, params Type[] componentTypes)
//        {
//            if (op == Operator.And)
//            {
//                return FilterEntitiesAnd(componentTypes);
//            }
//            else
//            {
//                return FilterEntitiesOr(componentTypes);
//            }
//        }

//        private Entities FilterEntitiesAnd(params Type[] componentTypes)
//        {
//            var list = new Entities();
//            foreach (ComponentsSet componentsSet in _entityComponents)
//            {
//                if (componentsSet.IsDeleted) continue;
//                bool match = AllComponentsExistInComponentsSet(componentTypes, componentsSet);

//                if (match)
//                {
//                    list.AddEntity(componentsSet);
//                }
//            }

//            return list;
//        }

//        private Entities FilterEntitiesOr(params Type[] componentTypes)
//        {
//            var list = new Entities();
//            foreach (ComponentsSet componentsSet in _entityComponents)
//            {
//                if (componentsSet.IsDeleted) continue;
//                bool match = AtLeastOneComponentExistsInComponentsSet(componentTypes, componentsSet);

//                if (match)
//                {
//                    list.AddEntity(componentsSet);
//                }
//            }

//            return list;
//        }

//        private bool AllComponentsExistInComponentsSet(Type[] componentTypes, ComponentsSet componentsSet)
//        {
//            foreach (Type componentType in componentTypes)
//            {
//                // does component exist in componentsSet?
//                bool foundComponent = componentsSet.HasComponent(componentType);

//                if (!foundComponent)
//                {
//                    return false;
//                }
//            }

//            return true;
//        }

//        private bool AtLeastOneComponentExistsInComponentsSet(Type[] componentTypes, ComponentsSet componentsSet)
//        {
//            foreach (Type componentType in componentTypes)
//            {
//                // does component exist in componentsSet?
//                bool foundComponent = componentsSet.HasComponent(componentType);

//                if (foundComponent)
//                {
//                    return true;
//                }
//            }

//            return false;
//        }

//        public ComponentsSet this[int entityId] => GetEntity(entityId);

//        public IEnumerator<ComponentsSet> GetEnumerator()
//        {
//            foreach (ComponentsSet item in _entityComponents)
//            {
//                yield return item;
//            }
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }
//    }

//    public enum Operator
//    {
//        And,
//        Or
//    }
//}