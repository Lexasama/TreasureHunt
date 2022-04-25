using TreasureHunt.Game.Orientation;

namespace TreasureHunt.Game.Board;

public class GameBoard
{
    private readonly List<Player> _players = new();
     private readonly List<IBoardElement> _orderedBoardElements = new ();
     
     public List<IBoardElement>[,] Elements { get; }
     public GameBoard(int width, int height)
     {
         Elements = new List<IBoardElement>[width, height];
         for (int x = 0; x < width; x++)
         {
             for (int y = 0; y < height; y++)
             {
                 Elements[x, y] = new List<IBoardElement>();
             }
         }
     }

     public Player AddPlayer(string name, CardinalPoints orientation, int[] position)
     {
         Player player = new(name, orientation, position, this);
         Elements[position[0], position[1]].Add(player);
         _players.Add(player);
         _orderedBoardElements.Add(player);
         return player;
     }
     
     public Treasure AddTreasureHide(int[] position, int numberOfTreasures)
     {
         Treasure hide = new(position, numberOfTreasures, this);
         Elements[position[0], position[1]].Add(hide);
         _orderedBoardElements.Add(hide);
         return hide;
     }

     public Mountain AddMountain(int[] position)
     {
         Mountain mountain = new(position);
         Elements[position[0], position[1]].Add(mountain);
         _orderedBoardElements.Add(mountain);
         return mountain;
     }
     
     
     public override string ToString()
     {
         var lines = new string[Elements.GetLength(1)];
         for (int x = 0; x < Elements.GetLength(0); x++)
         {
             for (int y = 0; y < Elements.GetLength(1); y++)
             {
                 if (Elements[x, y].Count > 0)
                 {
                     lines[y] += Elements[x, y][0].ToString() + " ";
                 }
                 else
                 {
                     lines[y] += ". ";

                 }
             }

         }

         return string.Join("\n", lines);
     }
     
    public string Serialized()
    {
        var boardElements = _orderedBoardElements;
        string mapSerializedInfo = $"C - {Elements.GetLength(1)} - {Elements.GetLength(0)}";

        List<string> boardElemenetsSerialized = boardElements.Select(be => be.Serialize()).ToList();
        boardElemenetsSerialized.Insert(0, mapSerializedInfo);
        return String.Join("\n", boardElemenetsSerialized);

    }
}