using UnityEngine;
using Zenject;

public class SinglePlayerSceneInitializer : MonoBehaviour
{
    private BoardData _boardData;
    private ICommandDispatcher _commandDispatcher;
    private SpawnBoardCommand _spawnBoardCommand;

    [Inject]
    private void Initialize(ICommandDispatcher commandDispatcher, BoardData boardData, SpawnBoardCommand spawnBoardCommand)
    {
        _commandDispatcher = commandDispatcher;
        _boardData = boardData;
        _spawnBoardCommand = spawnBoardCommand;

        _commandDispatcher.ExecuteCommand(_spawnBoardCommand);
    }
}