public class SpawnBoardCommand : CommandBase
{
    private readonly BoardData _boardData;

    public SpawnBoardCommand(BoardData boardData)
    {
        _boardData = boardData;
    }

    public override void Execute()
    {
        _boardData.SpawnBoardStrategy.SpawnBoard(_boardData.RowLengths);
    }
}