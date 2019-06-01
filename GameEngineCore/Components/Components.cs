using SpaceShooterUtilities;

namespace GameEngineCore.Components
{
    public class Components<T>
    {
        private readonly Bag<T> _components;

        public Components()
        {
            _components = new Bag<T>();
        }

        public void Add(T texture)
        {
            _components.Add(texture);
        }

        public T this[int entityId]
        {
            get => _components[entityId];
            set => _components[entityId] = value;
        }
    }
}