using Zenject;

public class SinglePlayerSceneInitializer
{
    [Inject]
    private ICommandDispatcher _commandDispatcher;
    [Inject]
    private SpawnBoardCommandFactory _spawnBoardCommandFactory;

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
        _commandDispatcher.ExecuteCommand(_spawnBoardCommandFactory.Create());
    }
}