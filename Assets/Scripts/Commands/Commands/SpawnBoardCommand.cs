public class SpawnBoardCommand : SynchronousCommand
{
    private BoardData _boardData;
    private TileFactory _tileFactory;

    public SpawnBoardCommand(BoardData boardData, TileFactory tileFactory)
    {
        _boardData = boardData;
        _tileFactory = tileFactory;
    }

    public override void Execute()
    {
        _boardData.Strategy.SpawnBoard(_boardData.RowLengths, _tileFactory);
    }
}