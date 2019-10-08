using UnityEngine;
using Zenject;

public sealed class GameManager : MonoBehaviour
{
    //CommandDispatcher
    [Inject]
    private readonly ICommandDispatcher _commandDispatcher;

    //Managers
    [Inject]
    private readonly ILocalizationManager _localizationManager;
    [Inject]
    private readonly IInputManager _inputManager;

    //Data
    [Inject]
    private readonly SettingsContainer _settingsContainer;

    //Factories
    [Inject]
    private readonly ApplySettingsCommandFactory _applySettingsCommandFactory;
    [Inject]
    private readonly InitializeSettingsCommandFactory _initializeSettingsCommandFactory;
    [Inject]
    private readonly InitializeLocationCommandFactory _initializeLocationCommandFactory;
    
    public void Start()
    {
        _commandDispatcher.ExecuteCommand(_initializeSettingsCommandFactory.Create());
        _commandDispatcher.ExecuteCommand(_initializeLocationCommandFactory.Create());
    }
}