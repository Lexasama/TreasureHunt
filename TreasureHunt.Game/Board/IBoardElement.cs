namespace TreasureHunt.Game.Board;

public interface IBoardElement
{
    public BoardElementType BoardElementType { get; }
    
    public bool IsBlocking { get; }
    
    public string Identifier { get; }

    public bool HasUpdated();
    
    public string Serialize();
    
}