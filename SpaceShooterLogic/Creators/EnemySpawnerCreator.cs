using GameEngineCore;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Creators
{
    public static class SpawnCreator
    {
        public static Entity Create()
        {
            var components = new ComponentsSet();
            components.AddComponent(typeof(SpawnComponent), new SpawnComponent(1000));

            var explosion = new Entity(components);

            return explosion;
        }
    }
}