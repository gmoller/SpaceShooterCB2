using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Systems
{
    public class SetBoundingBoxSystem : System
    {
        public SetBoundingBoxSystem(string name, GameState gameState) : base(name, gameState)
        {
        }

        protected override void ProcessOneEntity(int entityId, float deltaTime)
        {
            // gather data for selection
            var transform = GameState.GameData.Transforms[entityId];
            var volume = GameState.GameData.Volumes[entityId];

            // selection
            if (transform == null || volume == null) return;

            // process data
            // calculate new Bounding Box
            var t = transform.Value;
            var v = volume.Value;
            var origin = new Vector2(v.Width / 2.0f, v.Height / 2.0f);

            var newVolume = new Rectangle(
                (int)(t.Position.X - (int)origin.X),
                (int)(t.Position.Y - (int)origin.Y),
                v.Width,
                v.Height);

            // update data
            GameState.GameData.Volumes[entityId] = newVolume;
        }
    }
}