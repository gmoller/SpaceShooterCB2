namespace SpaceShooterLogic.Components
{
    public struct Volume2 : IGameComponent
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int EntityId { get; set; }

        public Volume2(int entityId, float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            EntityId = entityId;
        }
    }
}