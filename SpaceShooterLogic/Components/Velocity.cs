namespace SpaceShooterLogic.Components
{
    public struct Velocity2 : IGameComponent
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int EntityId { get; set; }

        public Velocity2(int entityId, float x, float y)
        {
            X = x;
            Y = y;
            EntityId = entityId;
        }
    }
}