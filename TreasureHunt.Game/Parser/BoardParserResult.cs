
using TreasureHunt.Game.Board;

namespace TreasureHunt.Game.Parser;

public class BoardParserResult
{
    public GameBoard Board { get; set; }
    public List<ParsedPlayer> PlayersWithMoves { get; set; }
}