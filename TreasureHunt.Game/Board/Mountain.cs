namespace TreasureHunt.Game.Board;

public class Mountain : IBoardElement
{
    private readonly int[] _position;
    public BoardElementType BoardElementType { get; }
    public bool IsBlocking { get; }
    public string Identifier { get; }

    public Mountain(int[] position)
    {
        _position = position;
        BoardElementType = BoardElementType.MOUNTAIN;
        IsBlocking = true;
        Identifier = "M" + position[0] + "-" + position[1];
    }

    public bool HasUpdated()
    {
        return false;
    }

    public string Serialize()
    {
        return $"M - {_position[1]} - {_position[0]}";
    }
    
    public override string ToString()
    {
        return "M";
    }
}
