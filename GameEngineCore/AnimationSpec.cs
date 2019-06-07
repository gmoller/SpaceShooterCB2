namespace GameEngineCore
{
    public class AnimationSpec
    {
        public string SpriteSheet { get; set; }
        public int Duration { get; set; } // in milliseconds
        public int NumberOfFrames { get; set; }
        public bool Repeating { get; set; }

        public RectangleF[] Frames { get; set; }
    }
}