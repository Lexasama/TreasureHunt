using TreasureHunt.Game.Board;
using TreasureHunt.Game.Orientation;

namespace TreasureHunt.Game.Parser;

public class BoardParser
{
    public static BoardParserResult ParseBoardString(string boardString)
    {
        GameBoard context = null;
        
        var playerList = new List<ParsedPlayer>();
            foreach (var line in boardString.Split("\n"))
            {
                var cleanLine = line.Trim();
                if(cleanLine.StartsWith("#")) continue;

                var lineArgs = cleanLine.Split("-").Select(a => a.Trim()).ToList();

                if (lineArgs[0] == "C")
                {
                    context = new GameBoard(int.Parse(lineArgs[1]), int.Parse(lineArgs[2]));
                    continue;
                }

                if (context == null)
                    throw new ArgumentException("Map is not initialized before entities");

                if (lineArgs[0] == "M")
                {
                    context.AddMountain(new[] {int.Parse(lineArgs[1]), int.Parse(lineArgs[2])});
                } else if (lineArgs[0] == "T")
                {
                    context.AddTreasureHide(new[] {int.Parse(lineArgs[1]), int.Parse(lineArgs[2])},
                        int.Parse(lineArgs[3]));
                } else if (lineArgs[0] == "A")
                {
                    CardinalPoints orientation;
                    switch (lineArgs[4])
                    {
                        case "N":
                            orientation = CardinalPoints.NORTH;
                            break;
                        case "S":
                            orientation = CardinalPoints.SOUTH;
                            break;
                        case "E":
                            orientation = CardinalPoints.EAST;
                            break;
                        case "O":
                            orientation = CardinalPoints.WEST;
                            break;
                        default:
                            throw new ArgumentException(lineArgs[4] + " is not a valid orientation.");
                    }

                    Player p = context.AddPlayer(lineArgs[1], orientation, new[] {int.Parse(lineArgs[2]), int.Parse(lineArgs[3])});
                    ParsedPlayer parsedPlayer = new ()
                    {
                        Player = p,
                        Moves = lineArgs[5]
                    };
                    playerList.Add(parsedPlayer);
                }

            }

            var result = new BoardParserResult();
            result.Board = context;
            result.PlayersWithMoves = playerList;

            return result;
        }
    }
