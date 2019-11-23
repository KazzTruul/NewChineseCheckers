using Zenject;

public class SpawnBoardCommand : CommandBase
{
    [Inject]
    public BoardData BoardData;
    [Inject]
    private TileFactory _tileFactory;

    public override void Execute()
    {
        BoardData.Strategy.SpawnBoard(BoardData.RowLengths, _tileFactory);
    }
}