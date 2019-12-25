using UnityEngine;
using Zenject;
using Middleware;

public sealed class MasterContainer : MonoBehaviour
{
    //CommandDispatcher
    [Inject]
    private readonly ICommandDispatcher _commandDispatcher;

    //Managers
    [Inject]
    private readonly ILocalizationManager _localizationManager;
    [Inject]
    private readonly IInputManager _inputManager;
    [Inject]
    private readonly PlayFabManager _playFabManager;

    //Data
    [Inject]
    private readonly SettingsContainer _settingsContainer;

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

        if (!string.IsNullOrEmpty(_settingsContainer.Settings.Username)
            && !string.IsNullOrEmpty(_settingsContainer.Settings.Password))
        {

        }
        else
        {
            _commandDispatcher.ExecuteCommand(_loadSceneCommandFactory.Create(Constants.LoginSceneIndex, false, true));
        }
    }
}