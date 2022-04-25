using NUnit.Framework;
using TreasureHunt.Game.Board;
using TreasureHunt.Game.Orientation;
using TreasureHunt.Game.Parser;

namespace TreasureHunt.Tests;

public class Class1
{

    [Test]
    public void TestGame()
    {
        //C - 3 - 4
        //M - 1 - 0
        //M - 2 - 1
        //T - 0 - 3 - 2
        //T - 1 - 3 - 3
        //A - Lara - 1 - 1 - S - AADADAGGA
        GameBoard gameContext = new(3, 4);
        gameContext.AddMountain(new[] {1, 0});
        gameContext.AddMountain(new[] {2, 1});
        gameContext.AddTreasureHide(new[] {0, 3}, 2);
        gameContext.AddTreasureHide(new[] { 1, 3, 3 }, 2);

        Player player = gameContext.AddPlayer("Lara", CardinalPoints.SOUTH, new[] {1, 1});
        string sequencePlayer = "AADADAGGA";

        foreach (var move in sequencePlayer)
        {
            player.Move(move.ToString());
        }

        string result = gameContext.ToString();

        Console.WriteLine(result);

        string serialized = gameContext.Serialized();
        Console.WriteLine(serialized);

    }

    [Test]
    public void TestParser()
    {
        string testFileContent = File.ReadAllText("./TestFiles/T1.txt");
        BoardParserResult parsedFile = BoardParser.ParseBoardString(testFileContent);

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

        Console.WriteLine(parsedFile.Board.ToString());
    }
}
