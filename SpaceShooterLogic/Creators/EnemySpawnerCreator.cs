using GameEngineCore;
using SpaceShooterLogic.Components;

namespace SpaceShooterLogic.Creators
{
    public static class SpawnCreator
    {
        public static Entity2 Create()
        {
            var components = new ComponentsSet();
            components.AddComponent(typeof(SpawnComponent), new SpawnComponent(1000));

            var explosion = new Entity2(components);

            return explosion;
        }
    }
}