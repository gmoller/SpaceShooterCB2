using System;
using System.Collections;
using GameEngineCore.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngineCore
{
    public sealed class Registrar
    {
        private static readonly Lazy<Registrar> Lazy = new Lazy<Registrar>(() => new Registrar());

        public static Registrar Instance => Lazy.Value;

        private readonly Entities _entities;

        private Hashtable _components;

        public int EntityCount { get; private set; }

        private Registrar()
        {
            CreateNewHashtable();

            _entities = new Entities();
        }

        private void CreateNewHashtable()
        {
            EntityCount = 0;
            _components = new Hashtable
            {
                {"Textures", new Components<Texture2D>()},
                {"Positions", new Components<Vector2?>()},
                {"Sizes", new Components<Vector2?>()},
                {"Frames", new Components<Rectangle?>()},
                {"Rotations", new Components<float?>()},
                {"Velocities", new Components<Vector2?>()},
                {"PlayerInputTags", new Components<object>()},
                {"Volumes", new Components<Rectangle?>()},
                {"AnotherOne", new Components<Vector2?>()}
            }; // TODO: look into Dictionary<string, dynamic>
        }

        public void AddPlayerEntity(Texture2D texture, Vector2 position, Vector2 size, Rectangle frame, float rotation, Vector2 velocity)
        {
            ((Components<Texture2D>)_components["Textures"]).Add(texture);
            ((Components<Vector2?>)_components["Positions"]).Add(position);
            ((Components<Vector2?>)_components["Sizes"]).Add(size);
            ((Components<Rectangle?>)_components["Frames"]).Add(frame);
            ((Components<float?>)_components["Rotations"]).Add(rotation);
            ((Components<Vector2?>)_components["Velocities"]).Add(velocity);
            ((Components<object>)_components["PlayerInputTags"]).Add(new object());
            ((Components<Rectangle?>)_components["Volumes"]).Add(new Rectangle(0, 0, (int)size.X, (int)size.Y));
            ((Components<Vector2?>)_components["AnotherOne"]).Add(null);

            EntityCount++;
        }

        public T GetComponent<T>(string componentName, int entityId)
        {
            var components = (Components<T>) _components[componentName];

            return components[entityId];
        }

        public void SetComponent<T>(string componentName, int entityId, T value)
        {
            var components = (Components<T>)_components[componentName];

            components[entityId] = value;
        }

        #region Old
        public void Clear()
        {
            _entities.Clear();
            CreateNewHashtable();
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

        public Entities FilterEntities(Operator op, params Type[] componentTypes)
        {
            return _entities.FilterEntities(op, componentTypes);
        }
        #endregion
    }
}