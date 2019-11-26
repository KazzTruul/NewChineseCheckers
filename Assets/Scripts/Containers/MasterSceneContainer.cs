using UnityEngine;
using Zenject;

public class MasterSceneContainer : MonoBehaviour
{
    private ICommandDispatcher _commandDispatcher;
    private LoadSceneCommandFactory _loadSceneCommandFactory;

    [Inject]
    private void Initialize(ICommandDispatcher commandDispatcher, LoadSceneCommandFactory loadSceneCommandFactory)
    {
        _commandDispatcher = commandDispatcher;
        _loadSceneCommandFactory = loadSceneCommandFactory;

        _commandDispatcher.ExecuteCommand(_loadSceneCommandFactory.Create(Constants.MainMenuSceneIndex, true));
    }
}