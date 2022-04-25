namespace TreasureHunt.Game.Board;

public class Treasure: IBoardElement
{
    public BoardElementType BoardElementType { get; }
    public bool IsBlocking { get; }
    public string Identifier { get; }
    private readonly int[] _position;
    private int _treasureCount;
    private readonly GameBoard _ctx;

    public Treasure(int[] position, int treasuresCount, GameBoard ctx)
    {
        _position = position;
        _treasureCount = treasuresCount;
        _ctx = ctx;

        IsBlocking = false;
        Identifier = "T" + position[0] + position[1];
        BoardElementType = BoardElementType.TREASURE;
    }
    
    public bool HasUpdated()
    {
        if (_treasureCount != 0)
        {
            _treasureCount--;
            return true;
        }

        return false;
    }
    public string Serialize()
    {
        return $"T - {_position[1]} - {_position[0]} - {_treasureCount}";
    }

    public override string ToString()
    {
        return $"T({_treasureCount})";
    }
}