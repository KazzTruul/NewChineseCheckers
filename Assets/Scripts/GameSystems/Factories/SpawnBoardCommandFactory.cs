using Zenject;

public class SpawnBoardCommandFactory
{
    [Inject]
    private readonly BoardData _boardData;
    [Inject]
    private readonly TileFactory _tileFactory;

    public SpawnBoardCommand Create()
    {
        return new SpawnBoardCommand(_boardData, _tileFactory);
    }
}