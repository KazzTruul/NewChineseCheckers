using Zenject;

public class SpawnBoardCommand : SynchronousCommand
{
    [Inject]
    private BoardData _boardData;
    [Inject]
    private TileFactory _tileFactory;

    public override void Execute()
    {
        _boardData.Strategy.SpawnBoard(_boardData.RowLengths, _tileFactory);
    }
}