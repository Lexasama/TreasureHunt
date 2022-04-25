namespace TreasureHunt.Game.Orientation;

public static class OrientationUtil
{
    public static string OrientationToString(CardinalPoints o)
    {
        switch (o)
        {
            case CardinalPoints.NORTH:
                return "N";
            case CardinalPoints.EAST:
                return "E";
            case CardinalPoints.WEST:
                return "O";
            case CardinalPoints.SOUTH:
                return "S";
            default:
                throw new ArgumentException("Incorrect Orientation");
        }
    }
}