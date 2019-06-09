using SpaceShooterLogic.Components;
using SpaceShooterLogic.Creators;

namespace SpaceShooterLogic.Systems
{
    public class RestorePlayerSystem : System
    {
        private readonly float _deathRotationSpeed;

        public RestorePlayerSystem(string name, GameState gameState) : base(name, gameState)
        {
            _deathRotationSpeed = 0.0075f;
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var player = GameState.Players[entityId];

            // selection
            if (player.IsNull() || player.Status != PlayerStatus.Destroyed) return;

            // process data
            var rotation = GameState.Rotations[entityId];
            rotation += _deathRotationSpeed * deltaTime;

            if (!player.DeathOnCooldown)
            {
                player.DeathCooldownTime = 0.0f;
                player.Lives--;
                rotation = 0.0f;
                player.Status = PlayerStatus.Alive;

                GameState.ClearState();

                PlayerCreator.Create(GameState, player.Score, player.Lives.GetValueOrDefault());
                SpawnCreator.Create(GameState);
            }

            player.DeathCooldownTime -= deltaTime;

            // update data
            GameState.Players[entityId] = player;
            GameState.Rotations[entityId] = rotation;
        }
    }
}