using System.Collections.Generic;

namespace SpaceShooterLogic.Systems
{
    public class Systems
    {
        private const int NUMBER_OF_THREADS = 16;

        private readonly List<System> _systems;
        private readonly GameState _gameState;

        public Systems(GameState gameState, params System[] systems)
        {
            _gameState = gameState;
            _systems = new List<System>();
            foreach (var system in systems)
            {
                _systems.Add(system);
            }
        }

        public void Update(float deltaTime)
        {
            // TODO: remove dead entities
            //_gameState.CompactEntities();

            foreach (var system in _systems)
            {
                system.Process(deltaTime, NUMBER_OF_THREADS);
            }
        }
    }
}