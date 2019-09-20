using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    //CommandDispatcher
    [Inject]
    private ICommandDispatcher _commandDispatcher;

    //Managers
    [Inject]
    private ILocalizationManager _localizationManager;

    //Containers
    [Inject]
    private SettingsContainer _settingsContainer;

    //Factories
    [Inject]
    private ApplySettingsCommandFactory _applySettingsCommandFactory;
    [Inject]
    private InitializeSettingsCommandFactory _initializeSettingsCommandFactory;
    [Inject]
    private InitializeLocationCommandFactory _initializeLocationCommandFactory;
    
    public void Start()
    {
        _commandDispatcher.ExecuteCommand(_initializeSettingsCommandFactory.Create());
        _commandDispatcher.ExecuteCommand(_initializeLocationCommandFactory.Create());
    }
}