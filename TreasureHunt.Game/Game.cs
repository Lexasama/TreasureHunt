using TreasureHunt.Game.Board;
using TreasureHunt.Game.Parser;

namespace TreasureHunt.Game;

public class Game
{
    private GameBoard Board { get; set; }

    public Game( string entryFile)
    {
        string fileContent = File.ReadAllText(entryFile);
        BoardParserResult parsedFile = BoardParser.ParseBoardString(fileContent);

        int currentUserIdx = 0;
        while (true)
        {
            if (parsedFile.PlayersWithMoves.Sum(p => p.Moves.Length) == 0) break;
            ParsedPlayer userWithMoves = parsedFile.PlayersWithMoves[currentUserIdx];
            userWithMoves.Player.Move(userWithMoves.Moves[0].ToString());
            userWithMoves.Moves = userWithMoves.Moves[1..];
            if (currentUserIdx + 1 < parsedFile.PlayersWithMoves.Count)
                currentUserIdx++;
            else currentUserIdx = 0;
        }

        Board = parsedFile.Board;
    }

    public async Task Save(string path)
    {
        if (Directory.Exists(path))
        {
            path = Path.GetFullPath(Path.Combine(path, "result.txt"));
        } 
            
        await File.WriteAllTextAsync(path, Board.Serialized());
    }
}