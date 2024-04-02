namespace Blocks.PathPoints
{
    public class FreezerPathPoint : PathPoint
    {
        public override PointType Type => PointType.Freezer;
        public override bool Soft => true;
    }
}
