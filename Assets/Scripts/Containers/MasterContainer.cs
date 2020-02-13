using UnityEngine;
using Zenject;

public sealed class MasterContainer : MonoBehaviour
{
    //CommandDispatcher
    [Inject]
    private readonly ICommandDispatcher _commandDispatcher;
    
    //Factories
    [Inject]
    private readonly InitializeSettingsCommandFactory _initializeSettingsCommandFactory;
    [Inject]
    private readonly InitializeLocationCommandFactory _initializeLocationCommandFactory;
    [Inject]
    private readonly LoadSceneCommandFactory _loadSceneCommandFactory;

    public void Start()
    {
        _commandDispatcher.ExecuteCommand(_initializeSettingsCommandFactory.Create());
        _commandDispatcher.ExecuteCommand(_initializeLocationCommandFactory.Create());

        _commandDispatcher.ExecuteCommand(_loadSceneCommandFactory.Create(Constants.LoginSceneIndex, false, true));
    }
}