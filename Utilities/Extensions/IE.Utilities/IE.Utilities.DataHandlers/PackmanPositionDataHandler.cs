using IE.Utilities.Common;

namespace IE.Utilities.DataHandlers
{
    public class PackmanPositionDataHandler
    {
        public PackmanDirection Direction { get; set; }
        public bool BeginPackmanMotionFromSettings { get; set; }
        public bool UpdateTextLocation { get; set; }

        public double PositionX { get; set; }
        public double PositionY { get; set; }
    }
}
