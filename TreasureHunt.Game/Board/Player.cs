using TreasureHunt.Game.Orientation;

namespace TreasureHunt.Game.Board;

public class Player: IBoardElement
{
    private readonly string _name;
    private readonly GameBoard _ctx;
    private CardinalPoints _cardinalPoints;
    private int[] _position;
    private int _treasuresFound;
    public BoardElementType BoardElementType { get; }
    public bool IsBlocking { get; }
    public string Identifier { get; }

    public Player(string name, CardinalPoints cardinalPoints, int[] position, GameBoard ctx)
    {
        _name = name;
        _cardinalPoints = cardinalPoints;
        _position = position;
        _ctx = ctx;

        BoardElementType = BoardElementType.PLAYER;
        Identifier = name;
        IsBlocking = true;
    }
    
    
    public List<IBoardElement>[,] Move(string moveLetter)
    {
        if ("DG".Contains(moveLetter))
        {
            int baseOrientation = (int)_cardinalPoints;
                
            int newOrientation = moveLetter == "D" ? (baseOrientation + 1) : (baseOrientation - 1);
            if (newOrientation < 0) newOrientation = 4;
            else if (newOrientation > 4) newOrientation = 0;

            _cardinalPoints = (CardinalPoints) newOrientation;
            return _ctx.Elements;
        }

        if (moveLetter != "A") throw new ArgumentException("Move cannot be recognized", moveLetter);

        var vector = GetVectorForGoMove();
        var supposedNewPosition = new [] {_position[0] + vector[0], _position[1] + vector[1]};

        List<IBoardElement> elementsAtNewPosition = _ctx.Elements[supposedNewPosition[0], supposedNewPosition[1]];

        bool canMove = true;
        foreach (var elementAtPosition in elementsAtNewPosition)
        {
            if(elementAtPosition.IsBlocking)
            {
                canMove = false;
                break;
            }
            var acted = elementAtPosition.HasUpdated();
            if (acted && elementAtPosition.BoardElementType.Equals(BoardElementType.TREASURE))
            {
                _treasuresFound++;
            }
        }

        if (canMove)
        {
            _ctx.Elements[_position[0], _position[1]] 
                = _ctx.Elements[_position[0], _position[1]].Where(e => e.Identifier != Identifier).ToList();

            _position = supposedNewPosition;
            elementsAtNewPosition.Add(this);

        }
        return _ctx.Elements;


    }
    
    private int[] GetVectorForGoMove()
    {
        switch (_cardinalPoints)
        {
            case CardinalPoints.EAST:
                return new [] {0, 1};
            case CardinalPoints.SOUTH:
                return new [] {1, 0};
            case CardinalPoints.WEST:
                return new[] {0, -1};
            case CardinalPoints.NORTH:
                return new[] {-1, 0};
            default:
                return new[] {0, 0};
        }
    }
    public bool HasUpdated()
    {
        return false;
    }

    
    public override string ToString()
    {
        return "A("+_name+")";
    }
    
    public string Serialize()
    {
        return $"A - {_name} - {_position[1]} - {_position[0]} - {OrientationUtil.OrientationToString(_cardinalPoints)} - {_treasuresFound}";
    }
}