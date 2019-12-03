using Zenject;

public class SpawnBoardCommandFactory
{
    [Inject]
    private BoardData _boardData;
    [Inject]
    private TileFactory _tileFactory;

    public SpawnBoardCommand Create()
    {
        return new SpawnBoardCommand(_boardData, _tileFactory);
    }
}