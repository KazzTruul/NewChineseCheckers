using Zenject;

public class SinglePlayerSceneInitializer
{
    private ICommandDispatcher _commandDispatcher;
    private SpawnBoardCommand _spawnBoardCommand;

    [Inject]
    private void Initialize(ICommandDispatcher commandDispatcher, SpawnBoardCommand spawnBoardCommand)
    {
        _commandDispatcher = commandDispatcher;
        _spawnBoardCommand = spawnBoardCommand;
    }

    public void OnActiveSceneChanged(ActiveSceneChangedSignal signal)
    {
        //TODO: Update check
        if(signal.NewSceneIndex != Constants.SinglePlayerSceneIndex)
        {
            return;
        }

        SpawnBoard();
    }

    private void SpawnBoard()
    {
        _commandDispatcher.ExecuteCommand(_spawnBoardCommand);
    }
}